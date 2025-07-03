namespace internshipProject1.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        public ICollection<RouteStop> RouteStops { get; set; }
    }
}
