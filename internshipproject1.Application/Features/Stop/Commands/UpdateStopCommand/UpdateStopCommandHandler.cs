using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
namespace internshipproject1.Application.Features.Stop.Commands.UpdateStopCommand
{
    public class UpdateStopCommandHandler : IRequestHandler<UpdateStopCommandRequest, UpdateStopCommandResponse>
    {
        private readonly IStopRepository _stopRepository;

        public UpdateStopCommandHandler(IStopRepository stopRepository) {
            _stopRepository = stopRepository;
        }

        public async Task<UpdateStopCommandResponse> Handle(UpdateStopCommandRequest request, CancellationToken cancellationToken) {
            var stop = await _stopRepository.GetByIdAsync(request.Id,cancellationToken);
            if (stop == null)
            {
                throw new KeyNotFoundException($"Stop with ID {request.Id} cannot be found.");
            }
            stop.Name = request.Name;
            stop.Latitude = request.Latitude;
            stop.Longitude = request.Longitude;
            await _stopRepository.UpdateAsync(stop,cancellationToken);
            return new UpdateStopCommandResponse {
                Id = request.Id,
                Name = request.Name,
                Message = $"Stop-{stop.Id} has updated successfully!"
            };
        } 
    }
}
