using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Queries.GetStopsCount
{
    public class GetStopsHandler : IRequestHandler<GetStopsCountRequest, GetStopsCountResponse>
    {
        private readonly IStopRepository _stopRepository;
        public GetStopsHandler(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }
        public async Task<GetStopsCountResponse> Handle(GetStopsCountRequest request, CancellationToken cancellationToken)
        {
            var stopsCount = await _stopRepository.TotalStopsCount(cancellationToken);
            var response = new GetStopsCountResponse
            {
                Count = stopsCount
            };
            return response;
        }
    }
}
