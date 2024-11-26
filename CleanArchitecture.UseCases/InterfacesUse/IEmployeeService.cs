using CleanArchitecture.Entities.Notifications;
using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.ProjectDtos.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IEmployeeService  : IGenericService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesByProjectIdAsync(int projectId);
        Task<EmployeeWorkloadDto> GetEmployeeWorkloadAsync(string employeeId);
        Task AssignEmployeeToProjectAsync(string employeeId, int projectId);
        Task RemoveEmployeeFromProjectAsync(string employeeId, int projectId);



        Task<EmployeeDto> GetByIdAsync(string id);
     

        Task<List<TaskProject>> GetAssignedTasksAsync(string employeeEmail);
        Task UpdateTaskStatusAsync(string employeeEmail, int taskId, string status);

     //   Task<List<Notification>> GetNotificationsAsync(string employeeEmail);
        Task MarkNotificationAsReadAsync(int notificationId);
    }
}
