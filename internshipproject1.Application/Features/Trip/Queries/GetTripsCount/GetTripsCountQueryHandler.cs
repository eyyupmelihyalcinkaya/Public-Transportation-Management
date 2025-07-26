using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Queries.GetTripsCount
{
    public class GetTripsCountQueryHandler : IRequestHandler<GetTripsCountQueryRequest,GetTripsCountQueryResponse>
    {
        private readonly ITripRepository _tripRepository;
        public GetTripsCountQueryHandler(ITripRepository tripRepository)
        { 
            _tripRepository = tripRepository;
        }

        public async Task<GetTripsCountQueryResponse> Handle(GetTripsCountQueryRequest request, CancellationToken cancellationToken)
        {
            int trips = await _tripRepository.TotalTripsCount(cancellationToken);
            var response = new GetTripsCountQueryResponse { 
                Count = trips
            };
            return response;
        }
    }
}
