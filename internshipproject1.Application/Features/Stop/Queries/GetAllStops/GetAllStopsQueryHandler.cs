using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Queries.GetAllStops
{
    public class GetAllStopsQueryHandler : IRequestHandler<GetAllStopsQueryRequest, List<GetAllStopsQueryResponse>>
    {
        private readonly IStopRepository _stopRepository;

        public GetAllStopsQueryHandler(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        public async Task<List<GetAllStopsQueryResponse>> Handle(GetAllStopsQueryRequest request, CancellationToken cancellationToken)
        { 
            var stops = await _stopRepository.GetAllAsync(cancellationToken);
            if (stops == null) { 
                throw new KeyNotFoundException(nameof(stops));
            }
            var responseList = stops.Select(stop => new GetAllStopsQueryResponse
            {
                Id = stop.Id,
                Name = stop.Name,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude
            }).ToList();
            return responseList;
        }
    }
}
