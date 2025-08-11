using internshipproject1.Application.Features.RoleMenuPermission.Queries.GetPermission;
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
            var query = new GetPermissionQueryRequest(roleId,menuId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
