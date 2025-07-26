using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Queries.GetRouteStopsCount
{
    public class GetRouteStopsCountHandler : IRequestHandler<GetRouteStopsCountRequest, GetRouteStopsCountResponse>
    {
        private readonly IRouteStopRepository _routeStopRepository;
        public GetRouteStopsCountHandler(IRouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }
        public async Task<GetRouteStopsCountResponse> Handle(GetRouteStopsCountRequest request, CancellationToken cancellationToken)
        {
            var count = await _routeStopRepository.TotalRouteStopsCount(cancellationToken);
            return new GetRouteStopsCountResponse { Count = count };
        }
    }
}
