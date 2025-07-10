using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Route.Queries.GetRoutesById
{
    public class GetRoutesByIdHandler : IRequestHandler<GetRoutesByIdRequest, GetRoutesByIdResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public GetRoutesByIdHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<GetRoutesByIdResponse> Handle(GetRoutesByIdRequest request, CancellationToken cancellationToken) {
            var route = await _routeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (route == null)
            {
                throw new KeyNotFoundException($"Route with ID {request.Id} not found.");
            }
            var response = new GetRoutesByIdResponse
            {
                Id = route.Id,
                Name = route.Name,
                Description = route.Description,
                StartLocation = route.StartLocation,
                EndLocation = route.EndLocation
            };
            return response;
        }
    }
}
