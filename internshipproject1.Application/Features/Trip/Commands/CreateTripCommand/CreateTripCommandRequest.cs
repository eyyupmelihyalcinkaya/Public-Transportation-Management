using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Trip.Commands.CreateTripCommand
{
    public class CreateTripCommandRequest : IRequest<CreateTripCommandResponse>
    {
        public int RouteId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string DayType { get; set; }
    }
}
