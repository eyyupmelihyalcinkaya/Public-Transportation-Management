namespace internshipproject1.Domain.Entities
{
    public class myRoute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }


        public ICollection<RouteStop> RouteStops { get; set; }
        public ICollection<Trip> Trips { get; set; }
    }
}
