using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.RouteStop.Queries.GetRouteStopById
{
    public class GetRouteStopByIdQueryHandler : IRequestHandler<GetRouteStopByIdQueryRequest, GetRouteStopByIdQueryResponse>
    {
        private readonly IRouteStopRepository _routeStopRepository;

        public GetRouteStopByIdQueryHandler(IRouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }

        public async Task<GetRouteStopByIdQueryResponse> Handle(GetRouteStopByIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var routeStop = await _routeStopRepository.GetByIdAsync(request.Id, cancellationToken);
            if (routeStop == null)
            {
                throw new KeyNotFoundException($"RouteStop with ID {request.Id} not found.");
            }
            var response = new GetRouteStopByIdQueryResponse
            { 
                Id = routeStop.Id,
                RouteId = routeStop.RouteId,
                StopId = routeStop.StopId,
                Order = routeStop.Order,
            };
            return response;

        }
    }
}
