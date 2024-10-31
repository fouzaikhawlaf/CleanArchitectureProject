using CleanArchitecture.UseCases.Dtos.UserProfileDtos;
using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        // GET: api/UserProfile/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(string id)
        {
            var profile = await _userProfileService.GetProfileByIdAsync(id);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        // GET: api/UserProfile/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProfileByUserId(string userId)
        {
            var profile = await _userProfileService.GetProfileByUserIdAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        // POST: api/UserProfile
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] UserProfileDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userProfileService.CreateProfileAsync(profileDto);
            return CreatedAtAction(nameof(GetProfileById), new { id = profileDto.Id }, profileDto);
        }

        // PUT: api/UserProfile/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(string id, [FromBody] UserProfileDto profileDto)
        {
            if (id != profileDto.Id)
                return BadRequest("Profile ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProfile = await _userProfileService.GetProfileByIdAsync(id);
            if (existingProfile == null)
                return NotFound();

            await _userProfileService.UpdateProfileAsync(profileDto);
            return NoContent();
        }

        // DELETE: api/UserProfile/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            var result = await _userProfileService.DeleteProfileAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
