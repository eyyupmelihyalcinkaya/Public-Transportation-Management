using Core.Entities;

namespace Core.DTOs
{
    public class RouteStopCreateDTO
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public int Order { get; set; }
    }
}
