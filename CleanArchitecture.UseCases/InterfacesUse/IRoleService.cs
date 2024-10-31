using CleanArchitecture.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IRoleService
    {
        Task AssignRoleToUserAsync(string userId, string roleId);
        Task<List<Role>> GetUserRolesAsync(string userId);
        Task<List<Employee>> GetUsersByRoleAsync(string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task CreateRoleAsync(string roleName);
        Task<List<Role>> GetAllRolesAsync();
        // Mettre à jour un rôle existant
        Task UpdateRoleAsync(string roleId, string newRoleName);
    }
}
