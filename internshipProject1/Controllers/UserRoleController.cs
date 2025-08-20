using Microsoft.AspNetCore.Mvc;
using MediatR;
using internshipproject1.Application.Features.UserRole.Queries.GetAll;
using internshipproject1.Application.Features.UserRole.Queries.GetRolesByUserId;
using internshipproject1.Application.Features.UserRole.Queries.GetUserRole;
using internshipproject1.Application.Features.UserRole.Queries.GetUsersByRoleId;
using internshipproject1.Application.Features.UserRole.Queries.IsUserInRole;
using internshipproject1.Application.Features.UserRole.Commands.AssignToRole;
using internshipproject1.Application.Features.UserRole.Commands.RemoveFromRole;
namespace internshipProject1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        //TODO: Implement UserRoleController methods
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllUserRoles")]
        public async Task<IActionResult> GetAllUserRoles()
        { 
           var result = await _mediator.Send(new GetAllUserRolesQueryRequest());
           return Ok(result);
        }

        [HttpGet("GetRolesByUserId")]
        public async Task<IActionResult> GetRolesByUserId([FromQuery] int userId)
        {
            var result = await _mediator.Send(new GetRolesByUserIdQueryRequest(userId));
            if (result == null)
            {
                return NotFound($"No roles found for user with ID {userId}.");
            }
            return Ok(result);
        }

        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRole([FromQuery] int userId, [FromQuery] int roleId)
        { 
            var result = await _mediator.Send(new GetUserRoleQueryRequest(userId, roleId));
            if (result == null)
            {
                return NotFound($"User role not found for User ID {userId} and Role ID {roleId}.");
            }
            return Ok(result);
        }

        [HttpGet("GetUsersByRoleId")]
        public async Task<IActionResult> GetUsersByRoleId([FromQuery] int roleId)
        {
            var result = await _mediator.Send(new GetUsersByRoleIdQueryRequest(roleId));
            if (result == null || !result.Any())
            {
                return NotFound($"No users found for role with ID {roleId}.");
            }
            return Ok(result);
        }

        [HttpGet("IsUserInRole")]
        public async Task<IActionResult> IsUserInRole([FromQuery] int userId, [FromQuery] int roleId)
        {
            var result = await _mediator.Send(new IsUserInRoleQueryRequest() {UserId=userId,RoleId=roleId });
            if (result == null)
            {
                return NotFound($"User role check failed for User ID {userId} and Role ID {roleId}.");
            }
            return Ok(result);
        }



        [HttpPost("AssignToRole")]
        public async Task<IActionResult> AssignToRole([FromQuery] int userId,int roleId)
        { 
            var role = await _mediator.Send(new AssignToRoleCommandRequest(userId,roleId));
            if (role == null)
            {
                return BadRequest("Failed to assign user to role.");
            }
            return CreatedAtAction(nameof(GetUserRole), new { userId = role.UserId, roleId = role.RoleId }, role);
        }

        [HttpDelete("RemoveFromrole")]
        public async Task<IActionResult> RemoveFromRole([FromQuery] int userId, [FromQuery] int roleId)
        { 
            var result = await _mediator.Send(new RemoveFromRoleCommandRequest() {UserId=userId,RoleId=roleId });
            if (result == null)
            {
                return NotFound($"Failed to remove user with ID {userId} from role with ID {roleId}.");
            }
            return NoContent();
        }
    }
}
