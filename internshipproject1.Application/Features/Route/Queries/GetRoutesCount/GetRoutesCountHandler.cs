using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Queries.GetRoutesCount
{
    public class GetRoutesCountHandler : IRequestHandler<GetRoutesCountRequest, GetRoutesCountResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public GetRoutesCountHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        public async Task<GetRoutesCountResponse> Handle(GetRoutesCountRequest request, CancellationToken cancellationToken)
        {
            var routesCount = await _routeRepository.TotalRoutesCount(cancellationToken);
            return new GetRoutesCountResponse
            {
                count = routesCount
            };

        }
    }
}
