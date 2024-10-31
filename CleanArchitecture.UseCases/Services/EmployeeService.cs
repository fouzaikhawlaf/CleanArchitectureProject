using CleanArchitecture.Entities.Notifications;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.EmployeeDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class EmployeeService : GenericService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<Employee> _userManager;

        public EmployeeService(IEmployeeRepository employeeRepository, UserManager<Employee> userManager)
            : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
        }

        // Get employees by project ID
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByProjectIdAsync(int projectId)
        {
            var employees = await _employeeRepository.GetEmployeesByProjectIdAsync(projectId);
            return employees.Select(EmployeeMapper.MapToDto);
        }

        // Get employee workload
        public async Task<EmployeeWorkloadDto> GetEmployeeWorkloadAsync(string employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID = {employeeId} not found.");
            }

            var workload = new EmployeeWorkloadDto
            {
                EmployeeId = int.Parse(employee.Id),
                Workload = employee.Projects.Count
            };

            return workload;
        }

        // Assign employee to project
        public async Task AssignEmployeeToProjectAsync(string employeeId, int projectId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID = {employeeId} not found.");
            }

            if (!employee.Projects.Any(p => p.Id == projectId))
            {
                var project = new Project { Id = projectId };
                employee.Projects.Add(project);
                await _employeeRepository.UpdateAsync(employee);
            }
        }

        // Remove employee from project
        public async Task RemoveEmployeeFromProjectAsync(string employeeId, int projectId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID = {employeeId} not found.");
            }

            var project = employee.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                employee.Projects.Remove(project);
                await _employeeRepository.UpdateAsync(employee);
            }
        }

        // Get tasks assigned to an employee
        public async Task<List<TaskProject>> GetAssignedTasksAsync(string employeeEmail)
        {
            var employee = await _userManager.FindByEmailAsync(employeeEmail);
           

            // Return tasks assigned to this employee
            return employee.AssignedTasks.ToList(); 
        }

        // Update the status of a task assigned to the employee
        public async Task UpdateTaskStatusAsync(string employeeEmail, int taskId, string status)
        {
            var employee = await _userManager.FindByEmailAsync(employeeEmail);
           

            // Find the task assigned to the employee
            var task = employee?.AssignedTasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                throw new System.Exception("Task not found.");
            }

            // Update the task's status
            if (Enum.TryParse(status, out TaskStatus taskStatus)) // Conversion string to TaskStatus
            {
                task.Status = taskStatus;
                await _userManager.UpdateAsync(employee); // Update the employee with the modified task
            }
            else
            {
                throw new ArgumentException("Invalid task status");
            }
        }

        // Implementing the abstract methods from GenericService
        protected override EmployeeDto MapToDto(Employee entity)
        {
            return EmployeeMapper.MapToDto(entity);
        }

        protected override Employee MapToEntity(EmployeeCreateDto createDto)
        {
            return EmployeeMapper.MapToEntity(createDto);
        }

        protected override void MapToEntity(EmployeeUpdateDto updateDto, Employee entity)
        {
            EmployeeMapper.MapToEntity(updateDto, entity);
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeCreateDto employeeCreateDto)
        {
            var employee = EmployeeMapper.MapToEntity(employeeCreateDto);
            employee.Id = Guid.NewGuid().ToString(); // Generate a new string ID
            await _employeeRepository.AddAsync(employee);
            return EmployeeMapper.MapToDto(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(EmployeeMapper.MapToDto);
        }

        public async Task<EmployeeDto> GetByIdAsync(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID = {id} not found.");
            }
            return EmployeeMapper.MapToDto(employee);
        }

        public async Task UpdateAsync(EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeUpdateDto.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID = {employeeUpdateDto.Id} not found.");
            }

            EmployeeMapper.MapToEntity(employeeUpdateDto, employee);
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID = {id} not found.");
            }

            await _employeeRepository.DeleteAsync(employee);
        }

        // Get all notifications for the employee
    /*    public async Task<List<Notification>> GetNotificationsAsync(string employeeEmail)
        {
            var employee = await _userManager.FindByEmailAsync(employeeEmail);
            if (employee == null || !employee.IsEmployee) // Correction: IsEmployee as a property
            {
                throw new System.Exception("Employee not found or not an employee.");
            }

            return employee.Notifications.OrderByDescending(n => n.CreatedAt).ToList();
        }*/

        // Mark notification as read
        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = _userManager.Users
                .SelectMany(u => u.Notifications)
                .FirstOrDefault(n => n.Id == notificationId);

            if (notification != null)
            {
                notification.IsRead = true;
                await _userManager.UpdateAsync(notification.User);
            }
        }
    }
}
