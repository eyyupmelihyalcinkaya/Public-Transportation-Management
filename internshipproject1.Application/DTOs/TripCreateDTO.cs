using internshipproject1.Domain.Entities;

namespace internshipproject1.Application.DTOs
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
