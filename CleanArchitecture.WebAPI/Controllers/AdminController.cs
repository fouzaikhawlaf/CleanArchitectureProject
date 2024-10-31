using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Dtos.UserDtos;
using CleanArchitecture.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.UseCases.Services;
using System.Net.Mail;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger; // Declare the logger
        public AdminController(IAdminService adminService , ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger; // Initialize the logger
        }

        // POST: api/admin/user/create
        [HttpPost("user/create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
                return BadRequest("Invalid user data.");

            try
            {
                var response = await _adminService.CreateUserAsync(createUserDto);

                // Only check if user creation was successful
                if (response.Employee == null)
                    return StatusCode(500, "User creation failed.");

                return CreatedAtAction(nameof(GetUserByEmail), new { email = response.Employee.Email }, response.Employee);
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "Error occurred while creating user.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // GET: api/admin/user/{email}
        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var employee = await _adminService.GetUserByEmailAsync(email);
                if (employee == null)
                    return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving user by email.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // PUT: api/admin/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null)
                return BadRequest("Invalid user data.");

            try
            {
                var result = await _adminService.UpdateUserAsync(id, updateUserDto);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("User ID cannot be null or empty.");
            }

            var result = await _adminService.DeleteUserAsync(userId);
            if (!result)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            return NoContent(); // 204 No Content
        }


        // GET: api/admin/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string searchTerm)
        {
            try
            {
                var users = await _adminService.SearchUsersAsync(searchTerm);
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        [HttpPost("user/create-profile")]
        public async Task<IActionResult> CreateProfile([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
                return BadRequest("Invalid user data.");

            try
            {
                // Création de l'instance Employee en utilisant les données de createUserDto
                var employee = new Employee
                {
                    Id = Guid.NewGuid().ToString(), // Utiliser un GUID pour l'ID de test
                    FirstName = createUserDto.FirstName,
                    LastName = createUserDto.LastName,
                    Email = createUserDto.Email,
                    UserName = createUserDto.Email
                };

                // Appel de la méthode CreateProfileForEmployee pour créer le profil
                await _adminService.CreateProfileForEmployee(employee, createUserDto);

                return Ok($"Profile created successfully for user {employee.Email}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating profile.");
                return StatusCode(500, $"Error during profile creation: {ex.Message}");
            }
        }













    }
}

