using internshipproject1.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using internshipproject1.Domain.Entities;
using internshipproject1.Application.Interfaces;
using MediatR;
using internshipproject1.Application.Features.Stop.Queries.GetStopById;
using internshipproject1.Application.Features.Stop.Queries.GetNearbyStops;
using internshipproject1.Application.Features.Stop.Commands.CreateStopCommand;
using internshipproject1.Application.Features.Stop.Commands.DeleteStopCommand;
using internshipproject1.Application.Features.Stop.Commands.UpdateStopCommand;
using internshipproject1.Application.Features.Stop.Queries.GetAllStops;
using internshipproject1.Application.Features.Stop.Queries.GetStopsCount;
namespace WebAPI.Controllers
{

  
    [ApiController]
    [Route("api/[controller]")]
    public class StopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StopsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Public API's

        //GET Stop by Id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStopById(int id) { 
        
            var response = await _mediator.Send(new GetStopByIdQueryRequest(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        //GET All Stops
        [HttpGet]
        public async Task<ActionResult> GetAllStops(int page = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetAllStopsQueryRequest());
            if (response == null || !response.Any())
            {
                return NotFound();
            }

            var totalCount = response.Count;
            var totalPages = (int)(Math.Ceiling((double)totalCount / pageSize));
            var pagedResponse = response
                .Skip((page -1)* pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(pagedResponse);
        }
        [HttpGet("TotalCount")]
        public async Task<ActionResult> StopsCount()
        {
            var result = await _mediator.Send(new GetStopsCountRequest());
            return Ok(result.Count);
        }
        //GET Nearby Stops
        [HttpGet("nearby")]
        public async Task<ActionResult> GetNearbyStops([FromQuery] double latitude, [FromQuery] double longitude) {
            var request = new GetNearbyStopsRequest(latitude, longitude);
            var response = await _mediator.Send(request);
            if (response == null || !response.Any())
            {
                return NotFound();
            }
            return Ok(response);
        }


        // Private API's

        // POST Create Stop
        [HttpPost]
        public async Task<ActionResult> CreateStop(CreateStopCommandRequest request) {
            var response = await _mediator.Send(request);
            if (response == null)
            {
                return BadRequest("Failed to create stop.");
            }
            return Ok(response);
        }

        //DELETE Stop By ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStopById(int id) { 
            var response = await _mediator.Send(new DeleteStopCommandRequest(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // PUT Update Stop
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStop(int id, UpdateStopCommandRequest request)
        {
            if (id != request.Id) { 
                return BadRequest("Stop ID mismatch.");
            }
            var response = await _mediator.Send(request);
            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
