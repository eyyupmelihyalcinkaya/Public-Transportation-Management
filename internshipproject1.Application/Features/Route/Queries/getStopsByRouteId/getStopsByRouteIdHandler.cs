using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Route.Queries.getStopsByRouteId
{
    public class getStopsByRouteIdHandler : IRequestHandler<getStopsByRouteIdRequest, List<getStopsByRouteIdResponse>?>
    {
        private readonly IRouteStopRepository _routeStopRepository;

        public getStopsByRouteIdHandler(IRouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }

        public async Task<List<getStopsByRouteIdResponse>?> Handle(getStopsByRouteIdRequest request, CancellationToken cancellationToken) { 
        
            var routeStops = await _routeStopRepository.GetAllByRouteIdAsync(request.RouteId);
            var orderedStops = routeStops.OrderBy(rs => rs.Order).Select(rs => new getStopsByRouteIdResponse 
            {
                Id = rs.Stop.Id,
                Name = rs.Stop.Name,
                Latitude = rs.Stop.Latitude,
                Longitude = rs.Stop.Longitude
            }).ToList();
            return orderedStops;
        }

    }
}
