using API.Errors;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound(new ApiResponse(404, $"Role with ID {id} not found."));
            }
            return Ok(role);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<Role>>> GetRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            if (roles == null || !roles.Any())
            {
                return NotFound(new ApiResponse(404, "No roles found."));
            }
            return Ok(roles);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Role>> CreateRole([FromBody]Role role)
        {
            if (role == null)
            {
                return BadRequest(new ApiResponse(400, "Role cannot be null."));
            }
            var createdRole = await _roleService.AddRoleAsync(role);
            return CreatedAtAction(nameof(GetRole), new { id = createdRole.Id }, createdRole);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Role>> UpdateRole(int id, [FromBody] Role role)
        {
            if (role == null || role.Id != id)
            {
                return BadRequest(new ApiResponse(400, "Role data is invalid."));
            }
            var existingRole = await _roleService.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                return NotFound(new ApiResponse(404, $"Role with ID {id} not found."));
            }
            var updatedRole = await _roleService.UpdateRoleAsync(role);
            return Ok(updatedRole);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var roleExists = await _roleService.RoleExistsAsync(id);
            if (!roleExists)
            {
                return NotFound(new ApiResponse(404, $"Role with ID {id} not found."));
            }
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }

        [HttpPost("{roleId}/permissions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Role>> AddPermission(int roleId, [FromBody] string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
            {
                return BadRequest(new ApiResponse(400, "Permission cannot be null or empty."));
            }
            var updatedRole = await _roleService.AddPermissionAsync(roleId, permission);
            return Ok(updatedRole);
        }
    }
}