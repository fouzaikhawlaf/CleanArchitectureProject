using CleanArchitecture.FrameworkAndDrivers.Exceptions;
using CleanArchitecture.UseCases.Dtos.UserDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var tokenDto = await _authService.LoginAsync(loginDto);

            if (tokenDto == null)
            {
                return Unauthorized("Invalid login attempt.");
            }

            return Ok(new
            {
                AccessToken = tokenDto.Token,
                Expiration = tokenDto.Expiration, // Ajoutez également cette propriété si nécessaire
                Username = tokenDto.Username // Ajoutez cette propriété si nécessaire
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var result = await _authService.ForgotPasswordAsync(email);
            if (!result)
            {
                return BadRequest("Failed to process the request.");
            }

            return Ok("Password reset token sent.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _authService.ResetPasswordAsync(resetPasswordDto);
            if (result)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest("Password reset failed.");
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                await _authService.ChangePasswordAsync(changePasswordDto);
                return Ok("Password has been changed successfully.");
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string username)
        {
            var result = await _authService.LogoutAsync(username);
            if (result)
            {
                return Ok("User logged out successfully.");
            }

            return BadRequest("Failed to log out user.");
        }
    }
}
