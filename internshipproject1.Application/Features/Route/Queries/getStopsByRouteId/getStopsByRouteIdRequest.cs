using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Route.Queries.getStopsByRouteId
{
    public class getStopsByRouteIdRequest : IRequest<List<getStopsByRouteIdResponse>>
    {
        public int RouteId { get; set; }
        public getStopsByRouteIdRequest(int routeId)
        {
            RouteId = routeId;
        }
    }
}
