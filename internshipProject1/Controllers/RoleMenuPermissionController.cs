using internshipproject1.Application.Features.RoleMenuPermission.Commands.AddPermission;
using internshipproject1.Application.Features.RoleMenuPermission.Commands.DeletePermission;
using internshipproject1.Application.Features.RoleMenuPermission.Commands.UpdatePermission;
using internshipproject1.Application.Features.RoleMenuPermission.Queries.GetAllowedMenusForRole;
using internshipproject1.Application.Features.RoleMenuPermission.Queries.GetMenusWithPermissionsForUser;
using internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermission;
using internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByMenuId;
using internshipproject1.Application.Features.RoleMenuPermission.Queries.HasPermission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RoleMenuPermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleMenuPermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetPermission")]
        public async Task<IActionResult> GetRoleMenuPermissions([FromQuery] int roleId, [FromQuery] int menuId)
        {
            var query = new GetPermissionQueryRequest(roleId, menuId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetPermissionByMenuId")]
        public async Task<ActionResult> GetPermissionsByMenuId(int menuId)
        {
            var result = await _mediator.Send(new GetPermissionByMenuIdQueryRequest(menuId));

            if (!result.HasAnyPermission)
            {
                return NotFound(new
                {
                    message = result.Message,
                    menuId = menuId,
                    permissions = result.RolePermissions
                });
            }

            return Ok(result);
        }
        [HttpGet("GetPermissionByRoleId")]
        public async Task<IActionResult> GetPermissionByRoleId([FromQuery] int roleId)
        {
            var permissions = await _mediator.Send(new internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermissionByRoleId.GetPermissionByRoleIdQueryRequest(roleId));
            if (permissions == null || !permissions.Any())
            {
                return NotFound(new { message = $"No permissions found for role {roleId}" });
            }
            return Ok(permissions);
        }
        [HttpGet("GetAllowedMenusForRole")]
        public async Task<IActionResult> GetAllowedMenusForRole([FromQuery] int roleId)
        {
            var menus = await _mediator.Send(new internshipproject1.Application.Features.RoleMenuPermission.Queries.GetAllowedMenusForRole.GetAllowedMenusForRoleQueryRequest(roleId, 0, "read"));
            if (menus == null)
            {
                return NotFound(new { message = $"No allowed menus found for role {roleId}" });
            }
            return Ok(menus);
        }

        [HttpGet("GetMenuWithPermissionsForUser")]
        public async Task<IActionResult> GetMenuWithPermissionsForUser([FromQuery] int userId)
        {
            var result = await _mediator.Send(new GetMenusWithPermissionsForUserQueryRequest(userId));
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No menus found for the specified user." });
            }
            return Ok(result);
        }
        [HttpGet("HasPermission")]
        public async Task<IActionResult> HasPermission([FromQuery] int userId, [FromQuery] int menuId, [FromQuery] string permissionType)
        {
            if (userId <= 0 || menuId <= 0 || string.IsNullOrWhiteSpace(permissionType))
            {
                return BadRequest(new { message = "Invalid parameters" });
            }
            var hasPermission = await _mediator.Send(new HasPermissionQueryRequest(userId, menuId, permissionType));
            return Ok(new { hasPermission = hasPermission.HasPermission, permissionType });
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission([FromBody] AddPermissionCommandRequest request)
        {
            var permission = await _mediator.Send(request);
            if (permission == null)
            {
                return BadRequest(new { message = "Permission could not be added." });
            }
            return Ok(new
            {
                message = "Permission added successfully.",
                permission = new
                {
                    permission.RoleId,
                    permission.MenuId,
                    permission.CanRead,
                    permission.CanCreate,
                    permission.CanUpdate,
                    permission.CanDelete
                }
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePermission([FromBody] UpdatePermissionCommandRequest request, CancellationToken cancellationToken)
        {
            var permission = await _mediator.Send(request, cancellationToken);
            if (permission == null)
            {
                return NoContent();
            }
            return Ok(permission);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePermission([FromQuery] int roleId, [FromQuery] int menuId)
        {
            var result = await _mediator.Send(new DeletePermissionCommandRequest() { RoleId = roleId, MenuId = menuId });
            if (result.IsSuccess)
            {
                return Ok(new { message = "Permission deleted successfully." });
            }
            return NotFound(new { message = "Permission not found." });

        }
    }
}
