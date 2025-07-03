namespace internshipProject1.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public myRoute Route { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayType { get; set; }

    }
}
