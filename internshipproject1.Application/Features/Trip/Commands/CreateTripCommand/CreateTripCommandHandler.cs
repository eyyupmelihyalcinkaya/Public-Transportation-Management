using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Trip.Commands.CreateTripCommand
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommandRequest, CreateTripCommandResponse>
    {
        private readonly ITripRepository _tripRepository;

        public CreateTripCommandHandler(ITripRepository tripRepository) { 
            _tripRepository = tripRepository;
        }

        public async Task<CreateTripCommandResponse> Handle(CreateTripCommandRequest request, CancellationToken cancellationToken) {

            var newTrip = new Domain.Entities.Trip
            {
                RouteId = request.RouteId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                DayType = request.DayType
            };

            await _tripRepository.AddAsync(newTrip);

            return new CreateTripCommandResponse
            {
                Id = newTrip.Id,
                RouteId = newTrip.RouteId,
                StartTime = newTrip.StartTime,
                EndTime = newTrip.EndTime,
                DayType = newTrip.DayType
            };
        
        }
    }
}
