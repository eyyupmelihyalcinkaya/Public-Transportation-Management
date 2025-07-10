using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using internshipproject1.Domain.Entities;
using internshipproject1.Application.Interfaces.Repositories;

namespace internshipproject1.Application.Features.Trip.Queries.GetTrip
{
    public class GetTripQueryHandler : IRequestHandler<GetTripQueryRequest, List<GetTripQueryResponse>>
    {
        private readonly ITripRepository _tripRepository;

        public GetTripQueryHandler(ITripRepository tripRepository) { 
            _tripRepository = tripRepository;
        }

        public async Task<List<GetTripQueryResponse>> Handle(GetTripQueryRequest request, CancellationToken cancellationToken) { 
            var trips = await _tripRepository.GetAllAsync(cancellationToken);

            var response = trips.Select(t=> new GetTripQueryResponse
            { 
                Id = t.Id,
                RouteId = t.RouteId,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                DayType = t.DayType
            }).ToList();
            return response;
        }
    }
}
