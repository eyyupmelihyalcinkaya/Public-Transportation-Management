using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Queries.GetAllRouteStops
{
    public class GetAllRouteStopsQueryRequest  : IRequest<List<GetAllRouteStopsQueryResponse>>
    {
    }
}
