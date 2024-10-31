using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanArchitecture.UseCases.InterfacesUse
{
    public interface IAuthService
    {

        // Authentication methods
        Task<TokenDto> LoginAsync(UserLoginDto loginDto);
     
      
        Task<bool> ForgotPasswordAsync(string email);

        // Password management methods
        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);
      
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

        // Logout
        Task<bool> LogoutAsync(string username);
    }
}
