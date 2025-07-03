namespace internshipProject1.Models
{
    public class myRoute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<RouteStop> RouteStops { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
