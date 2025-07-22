using internshipproject1.Application.Features.Trip.Commands.CreateTripCommand;
using internshipproject1.Application.Features.Trip.Commands.DeleteTripCommand;
using internshipproject1.Application.Features.Trip.Commands.UpdateTripCommand;
using internshipproject1.Application.Features.Trip.Queries.GetAllTrips;
using internshipproject1.Application.Features.Trip.Queries.GetTrip;
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
        public async Task<ActionResult> GettAllTrips(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllTripsQueryRequest(), cancellationToken);
            if (response == null || !response.Any())
            {
                return NotFound();
            }
            return Ok(response);
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
