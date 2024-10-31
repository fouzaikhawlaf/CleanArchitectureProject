﻿using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesByProjectIdAsync(int projectId);
        Task AddAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(string id);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }
    
    
}
