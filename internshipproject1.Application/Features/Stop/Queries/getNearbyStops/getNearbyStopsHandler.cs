using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Repositories;
using MediatR;

namespace internshipproject1.Application.Features.Stop.Queries.GetNearbyStops
{
    public class GetNearbyStopsHandler : IRequestHandler<GetNearbyStopsRequest, List<GetNearbyStopsResponse>>
    {
        private readonly IStopRepository _stopRepository;

        public GetNearbyStopsHandler(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

        public async Task<List<GetNearbyStopsResponse>> Handle(GetNearbyStopsRequest request, CancellationToken cancellationToken) { 
        
            var stops = await _stopRepository.GetAllAsync(cancellationToken);
            var nearbyStops = stops.Select(stops => new
            {
                Stop = stops,
                Distance = CalculateDistance(request.Latitude, request.Longitude, stops.Latitude, stops.Longitude)
            }).OrderBy(x => x.Distance).Select(x => new GetNearbyStopsResponse
            {
                Id = x.Stop.Id,
                Name = x.Stop.Name,
                Latitude = x.Stop.Latitude,
                Longitude = x.Stop.Longitude,
                Distance = double.Parse(x.Distance.ToString("F2")) // Format distance to 2 decimal places
            }).ToList();
            return nearbyStops;
        }

        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the Earth in KM.
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Pow(Math.Sin(dLat / 2), 2) + // Haversine Formula
                    Math.Cos(ToRadians(lat1)) *
                    Math.Cos(ToRadians(lat2)) *
                    Math.Pow(Math.Sin(dLon / 2), 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; //Distance in KM.
            return d;
        }
        private static double ToRadians(double deg) => deg * (Math.PI / 180);
    }




}
