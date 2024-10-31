using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IUserRepository
    {
     
        Task<Employee> GetUserByUserEmailAsync(string email);
        Task<Employee> GetUserByIdAsync(string userId);
      
        Task AddUserAsync(Employee user);
        Task UpdateUserAsync(Employee user);
        Task DeleteUserAsync(string id);
        Task<bool> IsPasswordInHistory(string userId, string password);
    }
}
