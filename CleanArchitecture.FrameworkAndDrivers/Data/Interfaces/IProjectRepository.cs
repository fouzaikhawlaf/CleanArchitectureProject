using CleanArchitecture.Entities.Projects;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IProjectRepository : IGenericRepository <Project>
    {
        Task<IEnumerable<Project>> GetProjectsByEmployeeIdAsync(int employeeId);
    }
    
    
}
