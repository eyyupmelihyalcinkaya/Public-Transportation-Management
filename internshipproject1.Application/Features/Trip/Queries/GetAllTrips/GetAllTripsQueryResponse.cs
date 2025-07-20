using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Queries.GetAllTrips
{
    public class GetAllTripsQueryResponse
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayType { get; set; }
    }
}
