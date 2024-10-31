// Services/AdminService.cs
using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Entities.Users;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Dtos.UserDtos;
using CleanArchitecture.FrameworksAndDrivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CleanArchitecture.UseCases.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        private readonly ILogger<AdminService> _logger;

        public AdminService(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, IRoleService roleService, IEmailService emailService, AppDbContext context, ILogger<AdminService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleService = roleService;
            _emailService = emailService;
            _context = context;
            _logger = logger;
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserDto createUserDto)
        {
            var response = new CreateUserResponse
            {
                EmailSent = false,
                Message = "User creation in progress."
            };

            try
            {
                // Check if the input DTO is null
                if (createUserDto == null)
                {
                    _logger.LogError("CreateUserDto is null.");
                    throw new ArgumentNullException(nameof(createUserDto));
                }

                // Log incoming user data
                _logger.LogInformation("Creating user with data: {@CreateUserDto}", createUserDto);

                // Validate required fields
                if (string.IsNullOrWhiteSpace(createUserDto.FirstName) ||
                    string.IsNullOrWhiteSpace(createUserDto.LastName) ||
                    string.IsNullOrWhiteSpace(createUserDto.Email) ||
                    string.IsNullOrWhiteSpace(createUserDto.Department) ||
                    string.IsNullOrWhiteSpace(createUserDto.Role))
                {
                    _logger.LogWarning("Missing required fields in CreateUserDto.");
                    throw new ArgumentException("All fields must be provided.");
                }

                // Create a new Employee object
                var employee = new Employee
                {
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    Email = createUserDto.Email,
                    Department= createUserDto.Department,
                    UserName = createUserDto.Email,
                };

                // Generate a default password for the new user
                var defaultPassword = GenerateDefaultPassword(createUserDto.Email);
                if (string.IsNullOrEmpty(defaultPassword))
                {
                    _logger.LogError("Generated password is null or empty.");
                    throw new Exception("Generated password is null or empty.");
                }

                // Attempt to create the user with the UserManager
                var result = await _userManager.CreateAsync(employee, defaultPassword);
                if (!result.Succeeded)
                {
                    var errorString = string.Join(", ", result.Errors.Select(e => e.Description));
                    response.Message = $"User creation failed: {errorString}";
                    _logger.LogError("User creation failed with errors: {ErrorString}", errorString);
                    return response;
                }

                // Get the newly created employee to ensure the ID is available
                var createdEmployee = await _userManager.FindByEmailAsync(createUserDto.Email);
                if (createdEmployee == null)
                {
                    _logger.LogError("Failed to retrieve the newly created employee.");
                    throw new Exception("Failed to retrieve the newly created employee.");
                }

                // Log successful user creation
                _logger.LogInformation("User created successfully with ID: {EmployeeId}", createdEmployee.Id);

                // Create a profile for the newly created user
                await CreateProfileForEmployee(createdEmployee, createUserDto);

                // Assign a role to the user
                await HandleRoleAssignment(createdEmployee, createUserDto.Role);

                // Send a welcome email with the generated password
                await SendCreationEmail(createdEmployee, defaultPassword);
                response.EmailSent = true;

                response.Employee = createdEmployee;
                response.Message = "User and profile created successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                response.Message = "An error occurred during user creation.";
            }

            return response;
        }



        private async Task HandleRoleAssignment(Employee employee, string roleName)
        {
            if (employee == null)
            {
                _logger.LogError("Employee object is null.");
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                _logger.LogError("Role name cannot be null or empty.");
                throw new ArgumentException("Role name cannot be null or empty.", nameof(roleName));
            }

            try
            {
                await _roleService.AssignRoleToUserAsync(employee.Id, roleName);
                _logger.LogInformation("Role {RoleName} assigned to user {UserEmail}.", roleName, employee.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to assign role {RoleName} to user {UserEmail}.", roleName, employee.Email);
                throw;
            }
        }


        private async Task<bool> SendCreationEmail(Employee employee, string defaultPassword)
        {
            try
            {
                // Prépare le sujet et le corps du message
                string subject = "Welcome to the ERP Application";
                string messageBody = $"Hello {employee.FirstName},\n\nYour account has been created successfully. Your default password is: {defaultPassword}\n\nBest regards,\nErpApplication Team";

                // Envoie l'email
                await _emailService.SendEmail(employee.Email, subject, messageBody);
                _logger.LogInformation($"Email sent successfully to {employee.Email}");
                return true;
            }
            catch (Exception emailEx)
            {
                // Log l'erreur si l'envoi échoue
                _logger.LogError(emailEx, "An error occurred while sending the email to {Email}", employee.Email);
                return false;
            }
        }



        public async Task CreateProfileForEmployee(Employee employee, CreateUserDto createUserDto)
        {
            if (employee == null || string.IsNullOrEmpty(employee.Id))
            {
                throw new ArgumentException("Employee data is missing or invalid.");
            }

            try
            {
                var profile = new UserProfile
                {
                    UserId = employee.Id, // Setting the UserId for the profile
                    User = employee // Setting the User navigation property
                };

                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Profile created successfully for User ID {employee.Id}.");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while creating profile for User ID {UserId}: {Message}", employee.Id, dbEx.Message);
                throw new Exception("Database error during profile creation.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General error while creating profile for User ID {UserId}: {Message}", employee.Id, ex.Message);
                throw new Exception("General error during profile creation.", ex);
            }
        }


        private string GenerateDefaultPassword(string userEmail)
        {
            var appName = "ShamashAppERP";
            var randomSuffix = Guid.NewGuid().ToString().Substring(0, 8);
            return $"{userEmail.Split('@')[0]}_{appName}_{randomSuffix}";
        }

        public async Task<Employee> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            // Trouver l'utilisateur par son ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false; // Retourner false si l'utilisateur n'est pas trouvé
            }

            // Trouver le UserProfile lié à cet utilisateur, s'il existe
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.UserId == userId);

            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile); // Supprimer le UserProfile lié si trouvé
            }

            // Supprimer l'utilisateur lui-même via l'UserManager
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return false; // Retourner false si la suppression de l'utilisateur a échoué
            }

            // Sauvegarder les changements dans la base de données
            await _context.SaveChangesAsync();

            return true; // Retourner true si l'opération a réussi
        }


        public async Task<bool> UpdateUserAsync(string userId, UpdateUserDto updateUserDto)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            if (employee == null) return false;

            employee.FirstName = updateUserDto.FirstName;
            employee.LastName = updateUserDto.LastName;
            employee.Email = updateUserDto.Email;
         

            var result = await _userManager.UpdateAsync(employee);
            return result.Succeeded;
        }

        public async Task<IEnumerable<Employee>> SearchUsersAsync(string searchTerm)
        {
            return await _context.Employees
                .Where(e => e.FirstName.Contains(searchTerm) || e.LastName.Contains(searchTerm) || e.Email.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
