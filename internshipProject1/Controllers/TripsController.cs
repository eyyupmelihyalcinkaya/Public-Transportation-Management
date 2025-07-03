using internshipProject1.Data;
using internshipProject1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using internshipProject1.Models;
namespace internshipProject1.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public TripsController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }


        // Public APIs
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrip([FromQuery] int routeId , string day){
            if(routeId < 0 || string.IsNullOrEmpty(day)){
                return BadRequest("Please enter a valid routeId and day");
            }
            var trips = await _dbContext.Trip.Where(t => t.RouteId == routeId && t.DayType.ToLower() == day.ToLower())
                .ToListAsync();
            if(trips == null || trips.Count == 0){
                return NotFound("Trip cannot found");
            }
            return Ok(trips);

        }


        //Private APIs

        [HttpPost]
        public async Task<ActionResult<Trip>> addStop([FromBody] TripCreateDTO dto)
        {
            var trip = new Trip
            {
                RouteId = dto.RouteId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                DayType = dto.DayType
        };
            _dbContext.Trip.Add(trip);
            await _dbContext.SaveChangesAsync();
            return Ok(trip);

        }

    }
}
