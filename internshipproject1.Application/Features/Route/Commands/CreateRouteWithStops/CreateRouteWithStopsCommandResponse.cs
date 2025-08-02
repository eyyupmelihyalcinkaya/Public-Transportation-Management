using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.DTOs;
using internshipproject1.Domain.Entities;
namespace internshipproject1.Application.Features.Route.Commands.CreateRouteWithStops
{
    public class CreateRouteWithStopsCommandResponse
    {
        public int Id { get; set; }
        public string RouteName { get; set; }
        public ICollection<RouteStopCreateDTO> Stops { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserResponseDTO CreatedBy { get; set; }
        
    }
}
