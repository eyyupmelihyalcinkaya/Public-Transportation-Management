using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Queries.GetAllRouteStops
{
    public class GetAllRouteStopsQueryResponse
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }
    }
}
