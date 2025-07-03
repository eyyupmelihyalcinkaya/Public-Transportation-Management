using internshipProject1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using internshipProject1.Models;
using internshipProject1.DTOs;
namespace internshipProject1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {


        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RoutesController(AppDbContext dbContext, IConfiguration configuration) {

            _dbContext = dbContext;
            _configuration = configuration;
        }


        // Public API's
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> getRoutes() {
            var routes = await _dbContext.Route.ToListAsync();
            return Ok(routes);  
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> getRoutesFromId(int id) { 
            var route = await _dbContext.Route.FindAsync(id);
            if (route == null) { return BadRequest("The route with the specified ID could not be found."); };
            return Ok(route);
        }
        [AllowAnonymous]
        [HttpGet("{id}/stops")]
        public async Task<ActionResult> getStops(int id) {
            var route = await _dbContext.Route.Include(r => r.RouteStops).
                ThenInclude(rs => rs.Stop).
                FirstOrDefaultAsync(r => r.Id == id);

            if (route == null) { return BadRequest("There is no Stops on the selected Route");};

            var stops = route.RouteStops.OrderBy(rs => rs.Order).Select(rs => new { rs.Stop.Id, rs.Stop.Name });

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
