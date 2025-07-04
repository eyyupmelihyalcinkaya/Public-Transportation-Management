using internshipProject1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using internshipProject1.Models;
using internshipProject1.DTOs;
using internshipProject1.Services.RedisService;
namespace internshipProject1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {


        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly RedisCacheService _cacheService;
        private readonly RedisCacheHelper _cacheHelper;


        public RoutesController(AppDbContext dbContext, IConfiguration configuration, RedisCacheService cacheService,RedisCacheHelper cacheHelper) {

            _dbContext = dbContext;
            _configuration = configuration;
            _cacheService = cacheService;
            _cacheHelper = cacheHelper;
        }


        // Public API's
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> getRoutes() {
            string cacheKey = "routeList";
            var cachedRoutes = await _cacheService.GetCacheAsync<List<myRoute>>(cacheKey);
            if (cachedRoutes != null) { 
                return Ok(cachedRoutes);  
            }
            var routes = await _dbContext.Route.ToListAsync();
            await _cacheService.SetCacheAsync(cacheKey, routes, TimeSpan.FromMinutes(15));
            return Ok(routes);  
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> getRoutesFromId(int id) {
            string cacheKey = $"route{id}";
            var cachedRoutes = await _cacheService.GetCacheAsync<myRoute>(cacheKey);
            if (cachedRoutes != null)
            {
                return Ok(cachedRoutes);
            }
            var route = await _dbContext.Route.FindAsync(id);
            if (route == null) { return BadRequest("The route with the specified ID could not be found."); };
            await _cacheService.SetCacheAsync(cacheKey, route, TimeSpan.FromMinutes(15));
            return Ok(route);
        }
        [AllowAnonymous]
        [HttpGet("{id}/stops")]
        public async Task<ActionResult> getStops(int id)
        {
            var cacheKey = $"stops:route:Route-{id}";

            var stops = await _cacheHelper.GetOrSetCacheAsync(cacheKey, async () =>
            {
                var route = await _dbContext.Route
                    .Include(r => r.RouteStops)
                    .ThenInclude(rs => rs.Stop)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (route == null)
                    return null;

                var stopList = route.RouteStops
                    .OrderBy(rs => rs.Order)
                    .Select(rs => new { rs.Stop.Id, rs.Stop.Name })
                    .ToList();

                return stopList;
            }, TimeSpan.FromMinutes(15));

            if (stops == null)
                return BadRequest("There is no Stops on the selected Route");

            return Ok(stops);
        }




        //Private API's
        [HttpPost]
        public async Task<ActionResult<Route>> addRoute([FromBody] RouteCreateDTO dto) {
            var route = new myRoute
            {
                Name = dto.Name,
                Description = dto.Description
            };
            
            if (route == null) {
                return BadRequest("Route cannot be Null");
            }
            _dbContext.Route.Add(route);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(getRoutesFromId), new { id = route.Id }, route);

        }

    }
}
