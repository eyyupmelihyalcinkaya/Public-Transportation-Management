namespace internshipProject1.Models
{
    public class RouteStop
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public myRoute Route { get; set; }
        public int StopId { get; set; }
        public Stop Stop { get; set; }
        public int Order { get; set; } 

    }
}
