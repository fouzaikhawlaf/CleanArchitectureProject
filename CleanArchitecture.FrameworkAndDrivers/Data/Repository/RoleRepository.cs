using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }


        // Get Role by Id
        public async Task<Role> GetByIdAsync(string roleId)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
        }

        // Get all roles
        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }


        public async Task CreateRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            var existingRole = await _context.Roles.FindAsync(role.RoleId);
            if (existingRole != null)
            {
                existingRole.RoleName = role.RoleName;  // Mise à jour du nom de rôle, si nécessaire
                _context.Roles.Update(existingRole);
                await _context.SaveChangesAsync();
            }
        }


    }
    
    
}
