using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;  
namespace internshipproject1.Application.Features.Stop.Commands.CreateStopCommand
{
    public class CreateStopCommandHandler : IRequestHandler<CreateStopCommandRequest, CreateStopCommandResponse>
    {
        private readonly IStopRepository _stopRepository;

        public CreateStopCommandHandler(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        public async Task<CreateStopCommandResponse> Handle(CreateStopCommandRequest request, CancellationToken cancellationToken) {

            var stop = new Domain.Entities.Stop
            {
                Name = request.StopName,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
            await _stopRepository.AddAsync(stop, cancellationToken);
            return new CreateStopCommandResponse
            {
                StopName = stop.Name,
                Latitude = stop.Latitude,
                Longitude = stop.Longitude
            };
        
        }
    }
}
