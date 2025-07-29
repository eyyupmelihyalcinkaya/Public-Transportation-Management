using internshipproject1.Application.Features.Trip.Commands.CreateTripCommand;
using internshipproject1.Application.Features.Trip.Commands.DeleteTripCommand;
using internshipproject1.Application.Features.Trip.Commands.UpdateTripCommand;
using internshipproject1.Application.Features.Trip.Queries.GetAllTrips;
using internshipproject1.Application.Features.Trip.Queries.GetTrip;
using internshipproject1.Application.Features.Trip.Queries.GetTripsCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TripsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Public API

        //GET Trip By ID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTripById(int id, CancellationToken cancellationToken) {
            var response = await _mediator.Send(new GetTripQueryRequest(id), cancellationToken);
            if (response == null || !response.Any())
            {
                return NotFound();
            }
            return Ok(response);
        }

        //GET All Trips
        [HttpGet]
        public async Task<ActionResult> GetAllTrips(CancellationToken cancellationToken, int page = 1, int pageSize = 10)
        {
            var response = await _mediator.Send(new GetAllTripsQueryRequest(), cancellationToken);
            if (response == null || !response.Any())
            {
                return NotFound();
            }
            int totalCount = response.Count();
            int pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
            var pagedResponse = response.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(pagedResponse);
        }

        [HttpGet("TotalCount")]
        public async Task<ActionResult> GetTripsCount(CancellationToken cancellationToken)
        {
            var trips = await _mediator.Send(new GetTripsCountQueryRequest(), cancellationToken);
            return Ok(trips.Count);
        }
        // Private API's

        //POST Create Trip
        [HttpPost]
        public async Task<ActionResult> CreateTrip(CreateTripCommandRequest request, CancellationToken cancellationToken)
        { 
            var response = await _mediator.Send(request, cancellationToken);
            if (response == null)
            {
                return BadRequest("Trip creation failed.");
            }
            return CreatedAtAction(nameof(GetTripById), new { id = response.Id }, response);
        }

        //DELETE Delete Trip By ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTripById(int id, CancellationToken cancellationToken)
        { 
            var response = await _mediator.Send(new DeleteTripCommandRequest(id), cancellationToken);
            if (response == null)
            {
                return NotFound("Trip not found.");
            }
            return NoContent();
        }

        //PUT Update Trip
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTrip(int id,UpdateTripCommandRequest request, CancellationToken cancellationToken)
        { 
            if (id != request.Id)
            {
                return BadRequest("Trip ID mismatch.");
            }
            var response = await _mediator.Send(request,cancellationToken);
            if (response == null)
            {
                return NotFound("Trip not found.");
            }
            return Ok(response);
        }


    }
}
