using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces;
namespace internshipproject1.Application.Features.Stop.Queries.GetStopById
{
    public class GetStopByIdQueryHandler : IRequestHandler<GetStopByIdQueryRequest, List<GetStopByIdQueryResponse>>
    {
        private readonly IStopRepository _stopRepository;
        private readonly IRedisCacheService _redisService;
    
        public GetStopByIdQueryHandler(IStopRepository stopRepository, IRedisCacheService redisService)
        {
            _stopRepository = stopRepository;
            _redisService = redisService;
        }

        public async Task<List<GetStopByIdQueryResponse>> Handle(GetStopByIdQueryRequest request, CancellationToken cancellationToken) { 
            var stops = await _stopRepository.GetByIdAsync(request.Id,cancellationToken);
            var response = new List<GetStopByIdQueryResponse> {
                new GetStopByIdQueryResponse{
                    Id = stops.Id,
                    Name = stops.Name,
                    Latitude = stops.Latitude,
                    Longitude = stops.Longitude
                }
            };

            return response;
        }

    }
}
