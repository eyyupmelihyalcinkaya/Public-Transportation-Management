using internshipproject1.Application.Features.Route.Queries.GetRoutes;
using internshipproject1.Application.Features.Route.Queries.GetRoutesById;
using internshipproject1.Application.Features.Route.Queries.GetStopsByRouteId;
using internshipproject1.Application.Features.Route.Commands.CreateRoute;
using internshipproject1.Application.Features.Route.Commands.UpdateRoute;
using internshipproject1.Application.Features.Route.Commands.DeleteRoute;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace internshipProject1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Public API's
        
        [HttpGet]
        public async Task<IActionResult> GetRoutes()
        {
            var response = await _mediator.Send(new GetRoutesQueryRequest());

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteById(int id)
        {
            var response = await _mediator.Send(new GetRoutesByIdRequest(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("{id}/stops")]
        public async Task<IActionResult> GetStopsByRouteId(int id)
        {
            var response = await _mediator.Send(new GetStopsByRouteIdRequest(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }



        //Private API's
        
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRoute(CreateRouteCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRouteById), new { id = response.Id }, response);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoute(int id, UpdateRouteCommandRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest("Route ID mismatch.");
            }
            var response = await _mediator.Send(command);
            if (response == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id) {
            var response = await _mediator.Send(new DeleteRouteCommandRequest() { Id = id });
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }



    }
}
