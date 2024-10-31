using CleanArchitecture.Entities.Users;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworksAndDrivers;
using CleanArchitecture.FrameworksAndDrivers.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace CleanArchitecture.FrameworkAndDrivers.Data.Repository
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        private readonly AppDbContext _dbcontext;

        public UserProfileRepository(AppDbContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<UserProfile> GetByIdAsync(string id)
        {
            return await _dbcontext.UserProfiles.FindAsync(id);
        }

        public async Task<UserProfile> GetByUserIdAsync(string userId)
        {
            return await _dbcontext.UserProfiles.FirstOrDefaultAsync(up => up.UserId == userId);
        }

        public async Task CreateAsync(UserProfile profile)
        {
            await _dbcontext.UserProfiles.AddAsync(profile);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile profile)
        {
            _dbcontext.UserProfiles.Update(profile);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var profile = await GetByIdAsync(id);
            if (profile != null)
            {
                _dbcontext.UserProfiles.Remove(profile);
                await _dbcontext.SaveChangesAsync();
            }
        }

      



    }

}
