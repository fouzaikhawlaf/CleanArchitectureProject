using CleanArchitecture.Entities.Projects;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly AppDbContext _dbContext;
        public ProjectRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Project>> GetProjectsByEmployeeIdAsync(int employeeId)
        {
            return await _dbContext.Projects
                .Include(p => p.Tasks)
                .Include(p => p.TeamMembers)
               .Where(p => p.TeamMembers.Any(e => e.Id == employeeId.ToString()))
                .ToListAsync();
        }


    }
}
