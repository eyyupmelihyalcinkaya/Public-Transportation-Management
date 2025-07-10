using internshipproject1.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using internshipproject1.Domain.Entities;
using internshipproject1.Application.Interfaces;
using MediatR;
using internshipproject1.Application.Features.RouteStop.Commands.AddRouteStop;
using internshipproject1.Application.Features.RouteStop.Queries.GetRouteStopById;
using internshipproject1.Application.Features.RouteStop.Commands.DeleteRouteStop;
using internshipproject1.Application.Features.RouteStop.Commands.UpdateRouteStop;
namespace WebAPI.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class RouteStopController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteStopController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Public API's


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteStopById(int id) { 
            var response = await _mediator.Send(new GetRouteStopByIdQueryRequest(id));
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }


        // Private API's

        //POST Create RouteStop
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateRouteStop(AddRouteStopCommandRequest request) {
            var response = await _mediator.Send(request);

            if (response == null)
            {
                return BadRequest("Failed to create route stop.");
            }
            return Ok(response);
        }

        //PUT Delete RouteStop
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRouteStop(int id)
        {
            var response = await _mediator.Send(new DeleteRouteStopCommandRequest() { Id = id });

            if (response == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //PUT Update RouteStop
        //[Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateRouteStop(UpdateRouteStopCommandRequest request) {
            var response = await _mediator.Send(request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);

        }
    }
}
