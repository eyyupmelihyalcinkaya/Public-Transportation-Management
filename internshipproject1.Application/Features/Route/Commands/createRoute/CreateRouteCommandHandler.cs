using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace internshipproject1.Application.Features.Route.Commands.createRoute
{
    public class CreateRouteCommandHandler : IRequestHandler<createRouteCommand, createRouteCommandResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public CreateRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<createRouteCommandResponse> Handle(createRouteCommand request, CancellationToken cancellationToken)
        {
            var route = new myRoute
            {
                Name = request.Name,
                Description = request.Description,
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation

            };
            var createdRoute = await _routeRepository.AddAsync(route);
            
            return new createRouteCommandResponse
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
