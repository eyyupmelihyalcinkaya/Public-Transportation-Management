using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace internshipproject1.Application.Features.Route.Commands.CreateRoute
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, CreateRouteCommandResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public CreateRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<CreateRouteCommandResponse> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            var route = new RouteToCreate
            {
                Name = request.Name,
                Description = request.Description,
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation

            };
            var createdRoute = await _routeRepository.AddAsync(route, cancellationToken); //cancellationToken
            
            return new CreateRouteCommandResponse
            {
                Id = createdRoute.Id,
                Name = createdRoute.Name,
                Description = createdRoute.Description,
                StartLocation = createdRoute.StartLocation,
                EndLocation = createdRoute.EndLocation
            };
        }
    }
}
