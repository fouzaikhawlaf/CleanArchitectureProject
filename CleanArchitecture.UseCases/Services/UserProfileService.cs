using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.UseCases.Dtos.UserProfileDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using CleanArchitecture.UseCases.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        // Injection du repository via le constructeur
        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // Créer un profil
        public async Task<UserProfileDto> CreateProfileAsync(UserProfileDto profileDto)
        {
            var profile = UserProfileMapper.MapToEntity(profileDto);
            await _userProfileRepository.CreateAsync(profile);
            return UserProfileMapper.MapToDto(profile);
        }

        // Mettre à jour un profil
        public async Task<UserProfileDto> UpdateProfileAsync(UserProfileDto profileDto)
        {
            var profile = UserProfileMapper.MapToEntity(profileDto);
            await _userProfileRepository.UpdateAsync(profile);
            return UserProfileMapper.MapToDto(profile);
        }

        // Récupérer les informations d'un profil par ID
        public async Task<UserProfileDto> GetProfileByIdAsync(string id)
        {
            var profile = await _userProfileRepository.GetByIdAsync(id);
            return UserProfileMapper.MapToDto(profile);
        }

        // Supprimer un profil par ID
        public async Task<bool> DeleteProfileAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var profile = await _userProfileRepository.GetByIdAsync(id);
            if (profile == null) return false;

            try
            {
                await _userProfileRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                // Log l'exception (si un système de log est disponible)
                // Logger.LogError(ex, "Error occurred while deleting the profile.");

                return false; // Retourner false si la suppression échoue
            }
        }




        public async Task<UserProfileDto> GetProfileByUserIdAsync(string userId)
        {
            // Appeler le repository pour obtenir le profil correspondant à l'ID utilisateur
            var profile = await _userProfileRepository.GetByUserIdAsync(userId);

            // Si un profil est trouvé, le mapper en DTO et le retourner
            if (profile != null)
            {
                return UserProfileMapper.MapToDto(profile);
            }

            // Retourner null si aucun profil n'est trouvé
            return null;
        }


    }

}
