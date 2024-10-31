using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        RoleManager<IdentityRole> _roleManager;
        public RoleService(IUserRepository userRepository, IRoleRepository roleRepository, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleToUserAsync(string userId, string roleId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user != null)
            {
                var role = await _roleRepository.GetByIdAsync(roleId);
                if (role != null)
                {
                    var userRole = new UserRole { UserId = userId, RoleId = roleId };
                    user.Roles.Add(userRole);
                    await _userRepository.UpdateUserAsync(user);
                }
            }
        }

        public async Task<List<Role>> GetUserRolesAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user?.Roles.Select(ur => ur.Role).ToList();
        }

        public async Task<List<Employee>> GetUsersByRoleAsync(string roleName)
        {
            var role = await _roleRepository.GetRoleByNameAsync(roleName);
            return role?.UserRoles.Select(ur => ur.User).ToList();
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task CreateRoleAsync(string roleName)
        {
            // Vérifier si le rôle existe déjà
            var existingRole = await _roleRepository.GetRoleByNameAsync(roleName);
            if (existingRole != null)
            {
                throw new InvalidOperationException("Role already exists");
            }

            // Créer un nouveau rôle
            var role = new Role { RoleName = roleName };
            await _roleRepository.CreateRoleAsync(role);
        }


        public async Task UpdateRoleAsync(string roleId, string newRoleName)
        {
            // Récupérer le rôle existant
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found");
            }

            // Mettre à jour le nom du rôle
            role.RoleName = newRoleName;
            await _roleRepository.UpdateRoleAsync(role);
        }


        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }


    }
}
