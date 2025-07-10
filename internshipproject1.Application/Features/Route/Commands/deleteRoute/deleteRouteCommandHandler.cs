using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Route.Commands.DeleteRoute
{
    public class deleteRouteCommandHandler : IRequestHandler<DeleteRouteCommandRequest, DeleteRouteCommandResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public deleteRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<DeleteRouteCommandResponse> Handle(DeleteRouteCommandRequest request, CancellationToken cancellationToken) { 
            var route = await _routeRepository.GetByIdAsync(request.Id,cancellationToken);
            if (route == null)
            {
                throw new Exception("The route with the specified ID could not be found.");
            }
            await _routeRepository.DeleteAsync(route, cancellationToken);
            return new DeleteRouteCommandResponse
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
