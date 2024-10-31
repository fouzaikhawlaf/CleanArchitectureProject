using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Mappers
{
    public static class UserMapper
    {
        public static async Task<UserDto> MapToDto(this Employee user, UserManager<Employee> userManager)
        {
            // Récupérer les rôles de l'utilisateur à l'aide de UserManager
            var roles = await userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = user.Department,
                Roles = new List<string>(roles), // Assigner les rôles récupérés
              
                Token = string.Empty // Initialize with empty string, update later
            };
        }

        public static Employee MapToEntity(this UserRegistrationDto userRegistrationDto)
        {
            return new Employee
            {
                Email = userRegistrationDto.Email,
                UserName = userRegistrationDto.Email,
                FirstName = userRegistrationDto.FirstName,
                LastName = userRegistrationDto.LastName,
                Department = userRegistrationDto.Department,
               
                IsArchived = false
            };
        }
    }
}
