using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByNameAsync(string roleName);
        Task<Role> GetByIdAsync(string roleId);
      
        Task<List<Role>> GetAllAsync();
        // Créer un nouveau rôle
        Task CreateRoleAsync(Role role);

        // Mettre à jour un rôle existant
        Task UpdateRoleAsync(Role role);
    }
}
