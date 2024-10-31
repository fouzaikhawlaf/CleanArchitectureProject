using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

    

        public async Task<Employee> GetUserByUserEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(Employee user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Employee user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsPasswordInHistory(string userId, string password)
        {
            // Récupérer les 5 derniers historiques de mots de passe pour l'utilisateur spécifié
            var passwordHistory = await _context.PasswordHistories
                .Where(ph => ph.UserId == userId)
                .OrderByDescending(ph => ph.ChangedAt)
                .Take(5)
                .ToListAsync();

            // Vérifier si l'un des mots de passe historiques correspond au mot de passe actuel
            foreach (var entry in passwordHistory)
            {
                // Utilisation directe de BCrypt.Net pour vérifier le mot de passe
                if (BCrypt.Net.BCrypt.Verify(password, entry.Password))
                {
                    return true;  // Le mot de passe correspond à un des anciens mots de passe
                }
            }

            // Aucun des mots de passe dans l'historique ne correspond
            return false;
        }


        public async Task<Employee> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
            return user;
        }

      
    }
}
