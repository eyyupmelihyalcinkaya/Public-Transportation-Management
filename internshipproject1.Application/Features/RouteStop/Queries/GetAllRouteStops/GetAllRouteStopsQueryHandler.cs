using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Queries.GetAllRouteStops
{
    public class GetAllRouteStopsQueryHandler : IRequestHandler<GetAllRouteStopsQueryRequest,List<GetAllRouteStopsQueryResponse>>
    {
        private readonly IRouteStopRepository _routeStopRepository;
        public GetAllRouteStopsQueryHandler(IRouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }
        public async Task<List<GetAllRouteStopsQueryResponse>> Handle(GetAllRouteStopsQueryRequest request, CancellationToken cancellationToken)
        {
            var routeStops = await _routeStopRepository.GetAllAsync(cancellationToken);
            return routeStops.Select(rs => new GetAllRouteStopsQueryResponse
            {
                Id = rs.Id,
                RouteId = rs.RouteId,
                StopId = rs.StopId,
                Order = rs.Order
            }).ToList();
        }
    }
}
