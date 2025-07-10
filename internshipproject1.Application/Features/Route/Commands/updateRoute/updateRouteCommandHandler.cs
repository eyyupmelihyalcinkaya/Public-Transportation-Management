using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Route.Commands.UpdateRoute
{
    public class updateRouteCommandHandler : IRequestHandler<UpdateRouteCommandRequest, UpdateRouteCommandResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public updateRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<UpdateRouteCommandResponse> Handle(UpdateRouteCommandRequest request, CancellationToken cancellationToken) {
            var route = await _routeRepository.GetByIdAsync(request.Id, cancellationToken);
            if(route == null)
            {
                throw new KeyNotFoundException($"Route with ID {request.Id} not found.");
            }
            route.Name = request.Name;
            route.Description = request.Description;
            route.StartLocation = request.StartLocation;
            route.EndLocation = request.EndLocation;

            var updatedRoute = await _routeRepository.UpdateAsync(route, cancellationToken);

            return new UpdateRouteCommandResponse
            {
                Id = updatedRoute.Id,
                Name = updatedRoute.Name,
                Description = updatedRoute.Description,
                StartLocation = updatedRoute.StartLocation,
                EndLocation = updatedRoute.EndLocation
            };
        }
    }
}
