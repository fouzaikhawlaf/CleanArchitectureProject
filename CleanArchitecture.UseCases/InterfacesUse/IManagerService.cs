using CleanArchitecture.Entities.Projects;
using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IManagerService
    {
        Task<List<Employee>> GetUserInDepartmentAsync(string managerEmail);
        Task AssignTaskToEmployeeAsync(string managerEmail, string employeeEmail, TaskProject task);
        Task<List<TaskProject>> GetTasksForDepartmentAsync(string managerEmail);
    }
}
