using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Features.Stop.Commands.UpdateStopCommand;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Trip.Commands.UpdateTripCommand
{
    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommandRequest, UpdateTripCommandResponse>
    {
        private readonly ITripRepository _tripRepository;

        public UpdateTripCommandHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<UpdateTripCommandResponse> Handle(UpdateTripCommandRequest request, CancellationToken cancellationToken) { 
            var trip = await _tripRepository.GetByIdAsync(request.Id, cancellationToken);
            trip.RouteId = request.RouteId;
            trip.StartTime = request.StartTime;
            trip.EndTime = request.EndTime;
            trip.DayType = request.DayType;
            await _tripRepository.UpdateAsync(trip, cancellationToken);

            return new UpdateTripCommandResponse
            {
                Id = trip.Id,
                RouteId = trip.RouteId,
                StartTime = trip.StartTime,
                EndTime = trip.EndTime,
                DayType = trip.DayType
            };
        }
    }
}
