using internshipproject1.Application.DTOs;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Commands.CreateRouteWithStops
{
    public class CreateRouteWithStopsCommandHandler : IRequestHandler<CreateRouteWithStopsCommandRequest, CreateRouteWithStopsCommandResponse>
    {
        private readonly IRouteRepository _routeRepository;
        public CreateRouteWithStopsCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        public async Task<CreateRouteWithStopsCommandResponse> Handle(CreateRouteWithStopsCommandRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RouteName))
                throw new ArgumentException("Route name is required.");

            if (request.Stops == null || !request.Stops.Any())
                throw new ArgumentException("At least one stop is required.");

            var createdRoute = new RouteToCreate
            {
                Name = request.RouteName,
                CreatedById = request.CreatedById,
                Description = request.Description,
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation
            };

            var newRoute = await _routeRepository.CreateRouteWithStops(createdRoute, request.Stops, cancellationToken);

            if (newRoute == null)
            {
                throw new InvalidOperationException("Route creation failed. Please check the provided data.");
            }

            if (newRoute.RouteStops == null)
            {
                throw new InvalidOperationException("Route was created but RouteStops collection is null.");
            }

            var response = new CreateRouteWithStopsCommandResponse
            {
                Id = newRoute.Id,
                RouteName = newRoute.Name,
                Stops = newRoute.RouteStops.Select(rs => new RouteStopCreateDTO
                {
                    Id = rs.Id,
                    StopName = rs.Stop?.Name ?? "Unknown",
                    Latitude = rs.Stop?.Latitude ?? 0,
                    Longitude = rs.Stop?.Longitude ?? 0,
                    Order = rs.Order
                }).ToList(),
                IsSuccess = true,
                Message = "Route with stops created successfully."
            };

            return response;
        }
    }
    
}
