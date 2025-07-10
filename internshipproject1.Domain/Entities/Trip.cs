namespace internshipproject1.Domain.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public RouteToCreate Route { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayType { get; set; }

    }
}
