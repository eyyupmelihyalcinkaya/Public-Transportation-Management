using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Queries.getNearbyStops
{
    public class getNearbyStopsRequest : IRequest<List<getNearbyStopsResponse>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public getNearbyStopsRequest(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
