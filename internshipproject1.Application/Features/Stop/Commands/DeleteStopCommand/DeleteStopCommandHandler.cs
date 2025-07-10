using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Stop.Commands.DeleteStopCommand
{
    public class DeleteStopCommandHandler : IRequestHandler<DeleteStopCommandRequest, DeleteStopCommandResponse>
    {
        private readonly IStopRepository _stopRepository;

        public DeleteStopCommandHandler(IStopRepository stopRepository) {
            _stopRepository = stopRepository;
        }

        public async Task<DeleteStopCommandResponse> Handle(DeleteStopCommandRequest request, CancellationToken cancellationToken) {
            var stop = await _stopRepository.GetByIdAsync(request.Id,cancellationToken);
            if (stop == null) {
                throw new KeyNotFoundException($"Stop with ID {request.Id} cannot be found.");
            }
            await _stopRepository.DeleteAsync(stop.Id, cancellationToken);
            return new DeleteStopCommandResponse { 
                Id = stop.Id,
                Name = stop.Name,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude
            };
        }
    }
}
