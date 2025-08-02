using internshipproject1.Application.DTOs;
using internshipproject1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Commands.CreateRouteWithStops
{
    public class CreateRouteWithStopsCommandRequest: IRequest<CreateRouteWithStopsCommandResponse>
    {
        public string RouteName { get; set; }
        public string Description { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public int CreatedById { get; set; }
        public List<StopCreateDTO> Stops { get; set; } = new List<StopCreateDTO>();
    }
    
}
