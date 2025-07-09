using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Commands.DeleteRouteStop
{
    public class DeleteRouteStopCommandResponse
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
