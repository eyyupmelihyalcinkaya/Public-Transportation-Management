using internshipProject1.Models;

namespace internshipProject1.DTOs
{
    public class TripCreateDTO
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayType { get; set; }
    }
}
