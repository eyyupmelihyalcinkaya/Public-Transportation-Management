using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Commands.DeleteTripCommand
{
    public class DeleteTripCommandResponse
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
