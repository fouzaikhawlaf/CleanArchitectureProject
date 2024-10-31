using CleanArchitecture.UseCases.InterfacesUse;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleRequest request)
        {
            await _roleService.AssignRoleToUserAsync(request.UserId, request.RoleId);
            return Ok();
        }

        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var roles = await _roleService.GetUserRolesAsync(userId);
            return Ok(roles);
        }

        [HttpGet("users-by-role/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var users = await _roleService.GetUsersByRoleAsync(roleName);
            return Ok(users);
        }

        // Endpoint pour créer un rôle
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            await _roleService.CreateRoleAsync(request.RoleName);
            return Ok("Role created successfully");
        }

        // Endpoint pour mettre à jour un rôle
        [HttpPut("update/{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRoleRequest request)
        {
            await _roleService.UpdateRoleAsync(roleId, request.NewRoleName);
            return Ok("Role updated successfully");
        }


        // Ajouter le point de terminaison pour récupérer tous les rôles
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }




    }

    // Classe pour les paramètres de la requête de création de rôle
    public class CreateRoleRequest
    {
        public string RoleName { get; set; }
    }

    // Classe pour les paramètres de la requête de mise à jour de rôle
    public class UpdateRoleRequest
    {
        public string NewRoleName { get; set; }
    }

    public class AssignRoleRequest
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
