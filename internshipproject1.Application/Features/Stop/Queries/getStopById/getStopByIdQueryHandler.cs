using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces;
namespace internshipproject1.Application.Features.Stop.Queries.getStopById
{
    public class getStopByIdQueryHandler : IRequestHandler<getStopByIdQueryRequest, List<getStopByIdQueryResponse>>
    {
        private readonly IStopRepository _stopRepository;
        private readonly IRedisCacheService _redisService;
    
        public getStopByIdQueryHandler(IStopRepository stopRepository, IRedisCacheService redisService)
        {
            _stopRepository = stopRepository;
            _redisService = redisService;
        }

        public async Task<List<getStopByIdQueryResponse>> Handle(getStopByIdQueryRequest request, CancellationToken cancellationToken) { 
            var stops = await _stopRepository.GetByIdAsync(request.Id);
            var response = new List<getStopByIdQueryResponse> {
                new getStopByIdQueryResponse{
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
