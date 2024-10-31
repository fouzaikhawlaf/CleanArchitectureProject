using CleanArchitecture.UseCases.Dtos.UserProfileDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IUserProfileService
    {
        Task<UserProfileDto> GetProfileByIdAsync(string id);
        Task<UserProfileDto> GetProfileByUserIdAsync(string  userId);
        Task<UserProfileDto> CreateProfileAsync(UserProfileDto profileDto);
        Task<UserProfileDto> UpdateProfileAsync(UserProfileDto profileDto);
        Task <bool> DeleteProfileAsync(string id);
      
    }

}
