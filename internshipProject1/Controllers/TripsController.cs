using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Services.RedisService;
using internshipProject1.Infrastructure.Data.Context;
namespace WebAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly RedisCacheService _cacheService;
        private readonly RedisCacheHelper _redisCacheHelper;
        public TripsController(AppDbContext dbContext, IConfiguration configuration,RedisCacheHelper redisCacheHelper,RedisCacheService redisCacheService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _redisCacheHelper = redisCacheHelper;
            _cacheService = redisCacheService;
        }


        // Public APIs
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrip([FromQuery] int routeId, string day) {
            if (routeId < 0 || string.IsNullOrEmpty(day)) {
                return BadRequest("Please enter a valid routeId and day");
            }
            string cacheString = $"Trip:Trips-{routeId}";
            var cachedTrips = await _redisCacheHelper.GetOrSetCacheAsync(cacheString, async () =>
                {
                    var trips = await _dbContext.Trip.Where(t => t.RouteId == routeId && t.DayType.ToLower() == day.ToLower())
                    .ToListAsync();
                    return trips;
                }, TimeSpan.FromMinutes(15)
            );
            if (cachedTrips == null)
            {
                return NotFound("Trip cannot found");
            }
            return Ok(cachedTrips);
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
