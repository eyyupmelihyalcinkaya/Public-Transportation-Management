using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Route.Queries.GetStopsByRouteId
{
    public class GetStopsByRouteIdRequest : IRequest<List<GetStopsByRouteIdResponse>>
    {
        public int RouteId { get; set; }
        public GetStopsByRouteIdRequest(int routeId)
        {
            RouteId = routeId;
        }
    }
}
