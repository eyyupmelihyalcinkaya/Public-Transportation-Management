using internshipproject1.Domain.Entities;

namespace internshipproject1.Application.DTOs
{
    public class RouteStopCreateDTO
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }
    }
}
