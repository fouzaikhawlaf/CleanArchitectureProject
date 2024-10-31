using CleanArchitecture.Entities.Projects;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    // ProjectManagement.Domain/Interfaces/ITaskRepository.cs
    public interface ITaskRepository : IGenericRepository<TaskProject>
    {
        Task<IEnumerable<TaskProject>> GetTasksByProjectIdAsync(int projectId);
        Task<IEnumerable<TaskProject>> GetTasksByEmployeeIdAsync(int employeeId);
    }
}
