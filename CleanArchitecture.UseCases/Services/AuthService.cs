using CleanArchitecture.Entities.Users;
using CleanArchitecture.UseCases.Dtos.UserDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using CleanArchitecture.FrameworkAndDrivers.Data.Interfaces;
using CleanArchitecture.FrameworkAndDrivers.Exceptions;
using CleanArchitecture.FrameworksAndDrivers;

namespace CleanArchitecture.UseCases.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly AppDbContext _context;

        public AuthService(
            UserManager<Employee> userManager,
            IConfiguration configuration,
            ILogger<AuthService> logger,
            IEmailService emailService,
            IUserRepository userRepository,
            IPasswordHasher<Employee> passwordHasher,
            AppDbContext context,
            TokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _context = context;
            _tokenService = tokenService;
        }

        // Authentification : Login
        public async Task<TokenDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUserEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return null; // Invalid login credentials
            }

            // Check if the user must change their password
            if (user.MustChangePassword)
            {
                return new TokenDto
                {
                    MustChangePassword = true,
                    Message = "User must change their password before logging in."
                };
            }

            // Generate the access and refresh tokens if the password change is not required
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Save the refresh token and set its expiry time
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // 7 days until expiration
            await _userManager.UpdateAsync(user);

            // Return the generated token along with other user info
            return new TokenDto
            {
                Token = accessToken,
                Expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"])),
                Username = user.UserName,
                MustChangePassword = false
            };
        }

        public async Task<bool> RevokeTokenAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new UserNotFoundException($"User with username '{username}' not found.");

            user.RefreshToken = null;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError($"Failed to revoke token for user '{username}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                return false;
            }

            _logger.LogInformation($"Successfully revoked token for user '{username}'.");
            return true;
        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserByIdAsync(changePasswordDto.Id);
            if (user == null) throw new UserNotFoundException("User not found.");

            if (!await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword))
                throw new UnauthorizedAccessException("Current password is incorrect.");

            if (await IsPasswordInRecentHistoryAsync(changePasswordDto.Id, changePasswordDto.NewPassword))
                throw new InvalidOperationException("New password was used recently. Please choose a different password.");

            var newPasswordHash = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
            user.PasswordHash = newPasswordHash;
            user.MustChangePassword = true;

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return true;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"{_configuration["AppUrl"]}/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendEmailAsync(email, "Reset Your Password", $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            return result.Succeeded;
        }

        public async Task<bool> LogoutAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return true;
        }

        private async Task<bool> IsPasswordInRecentHistoryAsync(string userId, string newPassword)
        {
            var passwordHistory = await _context.PasswordHistories
                .Where(ph => ph.UserId == userId)
                .OrderByDescending(ph => ph.DateAdded)
                .ToListAsync();

            return passwordHistory.Any(ph => _passwordHasher.VerifyHashedPassword(null, ph.Password, newPassword) == PasswordVerificationResult.Success);
        }
    }
}
