using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.RouteStop.Commands.DeleteRouteStop
{
    public class DeleteRouteStopCommandHandler : IRequestHandler<DeleteRouteStopCommandRequest, DeleteRouteStopCommandResponse>
    {
        private readonly IRouteStopRepository _routeStopRepository;

        public DeleteRouteStopCommandHandler(IRouteStopRepository routeStopRepository)
        {
            _routeStopRepository = routeStopRepository;
        }

        public async Task<DeleteRouteStopCommandResponse> Handle(DeleteRouteStopCommandRequest request, CancellationToken cancellationToken) { 
            var routeStop = await _routeStopRepository.GetByIdAsync(request.Id);
            if (routeStop == null)
            {
                throw new KeyNotFoundException("RouteStop not found.");
            }
            await _routeStopRepository.DeleteAsync(request.Id);
            return new DeleteRouteStopCommandResponse
            {
                Id = request.Id,
                RouteId = routeStop.RouteId,
                StopId = routeStop.StopId,
                Order = routeStop.Order,
                Message = "RouteStop deleted successfully."
            };

        }
    }
}
