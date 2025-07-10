using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Commands.UpdateRouteStop
{
    public class UpdateRouteStopCommandResponse
    {
        public int RouteStopId { get; set; }
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }

    }
}
