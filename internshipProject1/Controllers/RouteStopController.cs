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
    public class RouteStopController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public RouteStopController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }


        // Public APIs


        //Private APIs

        [HttpPost]
        public async Task<ActionResult<RouteStop>> addStop([FromBody] RouteStopCreateDTO dto)
        {
            var routeStop = new RouteStop
            {
                RouteId = dto.RouteId,
                StopId =dto.StopId,
                Order = dto.Order
            };
           /* if (routeStop.Stop == null) {
                return BadRequest("The Stop cannot found");
            }
            if (routeStop.Route == null)
            {
                return BadRequest("The Route cannot found");
            }*/
            if (routeStop == null) { return BadRequest("Stop informations cannot be null"); }

            _dbContext.RouteStop.Add(routeStop);
            await _dbContext.SaveChangesAsync();
            return Ok(routeStop);

        }

    }
}
