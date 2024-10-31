using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworksAndDrivers.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IUserProfileRepository :IGenericRepository<UserProfile>
    {
        Task<UserProfile> GetByIdAsync(string id);
        Task<UserProfile> GetByUserIdAsync(string userId);
        Task CreateAsync(UserProfile profile);
        Task UpdateAsync(UserProfile profile);
        Task DeleteAsync(string id);
    }

}
