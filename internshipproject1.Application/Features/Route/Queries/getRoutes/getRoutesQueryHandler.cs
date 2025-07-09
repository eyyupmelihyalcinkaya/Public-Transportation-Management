using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using internshipproject1.Application.Interfaces.Repositories;
namespace internshipproject1.Application.Features.Route.Queries.getRoutes
{
    public class getRoutesQueryHandler : IRequestHandler<getRoutesQueryRequest, List<getRoutesQueryResponse>>
    {

        private readonly IRouteRepository _routeRepository;

        public getRoutesQueryHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
        }
        public async Task<List<getRoutesQueryResponse>> Handle(getRoutesQueryRequest request, CancellationToken cancellationToken)
        {
            var routes = await _routeRepository.GetAllAsync();

            var response = routes.Select(r => new getRoutesQueryResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                StartLocation = r.StartLocation,
                EndLocation = r.EndLocation
            }).ToList();
            return response;
        }
    }
}
