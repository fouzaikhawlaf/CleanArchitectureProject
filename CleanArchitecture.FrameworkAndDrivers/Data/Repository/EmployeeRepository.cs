﻿using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using iText.Commons.Actions.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    // ProjectManagement.Infrastructure/Repositories/EmployeeRepository.cs
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByProjectIdAsync(int projectId)
        {
            return await _dbContext.Employees
                .Include(e => e.Projects)
                .Where(e => e.Projects.Any(p => p.Id == projectId))
                .ToListAsync();
        }





        public async Task AddAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            return await _dbContext.Employees.FindAsync(id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
