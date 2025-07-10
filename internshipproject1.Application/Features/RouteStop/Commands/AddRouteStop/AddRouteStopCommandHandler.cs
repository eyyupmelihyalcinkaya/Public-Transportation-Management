using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.RouteStop.Commands.AddRouteStop
{
    public class AddRouteStopCommandHandler : IRequestHandler<AddRouteStopCommandRequest, AddRouteStopCommandResponse>
    {
        private readonly IRouteStopRepository _routeStopRepository;


        public AddRouteStopCommandHandler(IRouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }

        public async Task<AddRouteStopCommandResponse> Handle(AddRouteStopCommandRequest request, CancellationToken cancellationToken) { 
            var routeStop = new Domain.Entities.RouteStop
            {
                RouteId = request.RouteId,
                StopId = request.StopId,
                Order = request.Order
            };
            var existingRouteStop = await _routeStopRepository.GetByRouteIdAndStopIdAsync(request.RouteId, request.StopId,cancellationToken);
            if (existingRouteStop is null)
            {
                throw new KeyNotFoundException("RouteStop already exists for this RouteId and StopId.");
            }
            await _routeStopRepository.AddAsync(routeStop, cancellationToken);
            return new AddRouteStopCommandResponse(routeStop.RouteId, routeStop.StopId, routeStop.Order);
        }
    }
}
