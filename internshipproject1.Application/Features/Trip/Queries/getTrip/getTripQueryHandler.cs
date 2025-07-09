using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using internshipproject1.Domain.Entities;
using internshipproject1.Application.Interfaces.Repositories;

namespace internshipproject1.Application.Features.Trip.Queries.getTrip
{
    public class getTripQueryHandler : IRequestHandler<getTripQueryRequest, List<getTripQueryResponse>>
    {
        private readonly ITripRepository _tripRepository;

        public getTripQueryHandler(ITripRepository tripRepository) { 
            _tripRepository = tripRepository;
        }

        public async Task<List<getTripQueryResponse>> Handle(getTripQueryRequest request, CancellationToken cancellationToken) { 
            var trips = await _tripRepository.GetAllAsync();

            var response = trips.Select(t=> new getTripQueryResponse
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
