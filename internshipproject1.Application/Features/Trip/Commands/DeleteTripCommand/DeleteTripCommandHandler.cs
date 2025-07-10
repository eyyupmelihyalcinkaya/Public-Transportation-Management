using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Trip.Commands.DeleteTripCommand
{
    public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommandRequest, DeleteTripCommandResponse>
    {
        private readonly ITripRepository _tripRepository;


        public DeleteTripCommandHandler(ITripRepository tripRepository) {
            _tripRepository = tripRepository;
        }

        public async Task<DeleteTripCommandResponse> Handle(DeleteTripCommandRequest request, CancellationToken cancellationToken) {
            var trip = await _tripRepository.GetByIdAsync(request.Id,cancellationToken);
            await _tripRepository.DeleteAsync(trip, cancellationToken);
            return new DeleteTripCommandResponse
            {
                Id = trip.Id,
                Message = $"Trip {trip.Id} Deleted Successfully"

            };
        }
    }
}
