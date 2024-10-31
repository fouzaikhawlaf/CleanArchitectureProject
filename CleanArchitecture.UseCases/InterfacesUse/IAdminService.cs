using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IAdminService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserDto createUserDto);


        Task<bool> DeleteUserAsync(string userId);
        Task<Employee> GetUserByEmailAsync(string email);
        Task<IEnumerable<Employee>> SearchUsersAsync(string searchTerm);
        Task<bool> UpdateUserAsync(string userId, UpdateUserDto updateUserDto);
        Task CreateProfileForEmployee(Employee employee, CreateUserDto createUserDto);
    }
}
