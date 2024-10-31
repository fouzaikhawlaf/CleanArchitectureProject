using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.UserProfileDtos;
using System;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class UserProfileMapper
    {
        // Méthode pour mapper un UserProfile en UserProfileDto
        public static UserProfileDto MapToDto(UserProfile profile)
        {
            if (profile == null) return null;

            return new UserProfileDto
            {
               
                UserId = profile.UserId,    // Assurez-vous que `UserId` dans le DTO est de type string
                FirstName = profile.FirstName,
                LastName = profile.LastName,
              
                PhoneNumber = profile.PhoneNumber,
                Address = profile.Address,
                DateOfBirth = profile.DateOfBirth,
              
            };
        }

        // Méthode pour mapper un UserProfileDto en UserProfile (pour créer ou mettre à jour un profil)
        public static UserProfile MapToEntity(UserProfileDto profileDto)
        {
            if (profileDto == null) return null;

            return new UserProfile
            {
               
                UserId = profileDto.UserId,    // Vérifiez bien que `UserId` dans le DTO est string
                FirstName = profileDto.FirstName,
                LastName = profileDto.LastName,
              
                PhoneNumber = profileDto.PhoneNumber,
                Address = profileDto.Address,
                DateOfBirth = profileDto.DateOfBirth,
               
               
            };
        }
    }
}
