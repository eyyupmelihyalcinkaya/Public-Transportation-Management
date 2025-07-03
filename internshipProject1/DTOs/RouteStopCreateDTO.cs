using internshipProject1.Models;

namespace internshipProject1.DTOs
{
    public class RouteStopCreateDTO
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }
    }
}
