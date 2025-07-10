using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using internshipproject1.Application.Interfaces.Repositories;
namespace internshipproject1.Application.Features.Route.Queries.GetRoutes
{
    public class GetRoutesQueryHandler : IRequestHandler<GetRoutesQueryRequest, List<GetRoutesQueryResponse>>
    {

        private readonly IRouteRepository _routeRepository;

        public GetRoutesQueryHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository ?? throw new ArgumentNullException(nameof(routeRepository));
        }
        public async Task<List<GetRoutesQueryResponse>> Handle(GetRoutesQueryRequest request, CancellationToken cancellationToken)
        {
            var routes = await _routeRepository.GetAllAsync(cancellationToken);

            var response = routes.Select(r => new GetRoutesQueryResponse
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
