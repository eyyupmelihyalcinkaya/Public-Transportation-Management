using Microsoft.AspNetCore.Mvc;
using MediatR;
using internshipproject1.Application.Features.Role.Queries.GetAll;
using internshipproject1.Application.Features.Role.Queries.GetById;
using internshipproject1.Application.Features.Role.Queries.IsExists;
using internshipproject1.Application.Features.Role.Commands.AddRole;
using internshipproject1.Application.Features.Role.Commands.UpdateRole;
using internshipproject1.Application.Features.Role.Commands.DeleteRole;
namespace internshipProject1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllRolesQueryRequest(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetRoleByIdQueryRequest(id));
            if (result == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpGet("IsRoleExist")]
        public async Task<IActionResult> IsRoleExist([FromQuery] int id)
        {
            var result = await _mediator.Send(new IsRoleExistsQueryRequest(id));
            return Ok(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return BadRequest("Failed to add role.");
            }
            return CreatedAtAction(nameof(GetRoleById), new { id = result.Id }, result);
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return BadRequest("Failed to update role.");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromQuery] int id, CancellationToken cancellationToken)
        {
            var isDeleted = await _mediator.Send(new DeleteRoleCommandRequest(id), cancellationToken);

            return NoContent();
        }
    }
}
