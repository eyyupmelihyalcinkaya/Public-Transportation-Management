using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.RouteStop.Commands.UpdateRouteStop
{
    public class UpdateRouteStopCommandHandler : IRequestHandler<UpdateRouteStopCommandRequest, UpdateRouteStopCommandResponse>
    {
        private readonly IRouteStopRepository _routeStopRepository;
    
        public UpdateRouteStopCommandHandler(IRouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }

        public async Task<UpdateRouteStopCommandResponse> Handle(UpdateRouteStopCommandRequest request, CancellationToken cancellationToken) { 
            var routeStop = await _routeStopRepository.GetByIdAsync(request.RouteStopId, cancellationToken);

            if(routeStop == null)
            {
                throw new Exception("Route stop not found");
            }
            routeStop.RouteId = request.RouteId;
            routeStop.StopId = request.StopId;
            routeStop.Order = request.Order;
            await _routeStopRepository.UpdateAsync(routeStop, cancellationToken);

            return new UpdateRouteStopCommandResponse
            {
                RouteStopId = routeStop.Id,
                RouteId = routeStop.RouteId,
                StopId = routeStop.StopId,
                Order = routeStop.Order
            };

        }


    }
}
