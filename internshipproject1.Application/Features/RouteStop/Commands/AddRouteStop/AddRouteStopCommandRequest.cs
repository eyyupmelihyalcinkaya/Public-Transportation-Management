using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.RouteStop.Commands.AddRouteStop
{
    public class AddRouteStopCommandRequest : IRequest<AddRouteStopCommandResponse>
    {
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }
        
    }
}
