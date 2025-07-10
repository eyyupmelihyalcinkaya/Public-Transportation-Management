using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Queries.GetNearbyStops
{
    public class GetNearbyStopsRequest : IRequest<List<GetNearbyStopsResponse>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public GetNearbyStopsRequest(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
