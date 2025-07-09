using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Route.Commands.deleteRoute
{
    public class deleteRouteCommandHandler : IRequestHandler<deleteRouteCommandRequest, deleteRouteCommandResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public deleteRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<deleteRouteCommandResponse> Handle(deleteRouteCommandRequest request, CancellationToken cancellationToken) { 
            var route = await _routeRepository.GetByIdAsync(request.Id);
            if (route == null)
            {
                throw new Exception("The route with the specified ID could not be found.");
            }
            await _routeRepository.DeleteAsync(route);
            return new deleteRouteCommandResponse
            {
                Id = route.Id,
                Name = route.Name,
                Description = route.Description,
                StartLocation = route.StartLocation,
                EndLocation = route.EndLocation
            };
        }
    }
}
