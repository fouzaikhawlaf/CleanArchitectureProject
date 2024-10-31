using CleanArchitecture.Entities.Projects;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class TaskRepository : GenericRepository<TaskProject>, ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Implement GetTasksByProjectIdAsync
        public async Task<IEnumerable<TaskProject>> GetTasksByProjectIdAsync(int projectId)
        {
            return await _dbContext.Set<TaskProject>()
                .Include(t => t.AssignedTo)
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        // Implement GetTasksByEmployeeIdAsync
        public async Task<IEnumerable<TaskProject>> GetTasksByEmployeeIdAsync(int employeeId)
        {
            return await _dbContext.Set<TaskProject>()
                .Include(t => t.Project)
                .Where(t => t.AssignedToId == employeeId.ToString()) // Conversion de l'int en string
                .ToListAsync();
        }
    }

}
