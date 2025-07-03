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
    public class StopsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public StopsController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2) {

            var R = 6371; // Radius of the Earth in KM.
            var dLat = ToRadians(lat2-lat1);
            var dLon = ToRadians(lon2-lon1);

            var a = Math.Pow(Math.Sin(dLat / 2),2) + // Haversine Formula
                    Math.Cos(ToRadians(lat1)) * 
                    Math.Cos(ToRadians(lat2)) *
                    Math.Pow(Math.Sin(dLon/2),2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; //Distance in KM.
            return d;
        }
        private static double ToRadians(double deg) => deg * (Math.PI / 180);


        // Public APIs
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> getStop(int id) {
            var stop = _dbContext.Stop.FindAsync(id);
            if (stop == null) {
                return NotFound("The Stop cannot found");
            }
            return Ok(stop);
        }
        [AllowAnonymous]
        [HttpGet("nearby")]
        public async Task<ActionResult<IEnumerable<Stop>>> nearbyStops([FromQuery] double lat, double lon) { 
            var stops = await _dbContext.Stop.ToListAsync();
            var nearbyStops = stops.Select(stop => new
            {
                Stop = stop,
                Distance = CalculateDistance(lat, lon, stop.Latitude, stop.Longitude)
            }).OrderBy(x => x.Distance).Select(x => new
            {
                x.Stop.Id,
                x.Stop.Name,
                x.Stop.Latitude,
                x.Stop.Longitude,
                DistanceInKm = x.Distance.ToString("F2")
            })
            .ToList();
            return Ok(nearbyStops);
        
        }

        //Private APIs

        [HttpPost("Create")]
        public async Task<ActionResult<Stop>> addStop([FromBody] StopCreateDTO dto) {
            var stop = new Stop
            {
                Name = dto.Name,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };
            if (stop == null) { return BadRequest("Stop informations cannot be null");}

            _dbContext.Stop.Add(stop);
            await _dbContext.SaveChangesAsync();
            return Ok(stop);

        } 
    
    }
}
