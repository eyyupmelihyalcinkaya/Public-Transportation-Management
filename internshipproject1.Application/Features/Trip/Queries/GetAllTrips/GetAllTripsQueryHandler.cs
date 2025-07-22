using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Queries.GetAllTrips
{
    public class GetAllTripsQueryHandler : IRequestHandler<GetAllTripsQueryRequest, List<GetAllTripsQueryResponse>>
    {
        private readonly ITripRepository _tripRepository;

        public GetAllTripsQueryHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<List<GetAllTripsQueryResponse>> Handle(GetAllTripsQueryRequest request, CancellationToken cancellationToken)
        {
            var trips = await _tripRepository.GetAllAsync(cancellationToken);
            if (trips == null || !trips.Any())
            {
                throw new KeyNotFoundException("No trips found.");
            }
            var listedTrips = trips.Select( t=> new GetAllTripsQueryResponse
            {
                Id = t.Id,
                RouteId = t.RouteId,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                DayType = t.DayType
            }).ToList();

            return listedTrips;
        }
    }
}
