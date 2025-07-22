using FluentValidation;
using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Commands.UpdateTripCommand
{
    public class UpdateTripCommandValidator : AbstractValidator<UpdateTripCommandRequest>
    {
        private readonly IRouteRepository _routeRepository;

        public UpdateTripCommandValidator(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;

            RuleFor(x=>x.RouteId)
                .NotEmpty().WithMessage("RouteId cannot be empty")
                .MustAsync(async (id, cancellation) => await _routeRepository.RouteExistByIdAsync(id, cancellation))
                .WithMessage("Route with this id does not exist");
            RuleFor(x=>x.StartTime)
                .NotEmpty().WithMessage("StartTime cannot be empty")
                .GreaterThan(TimeSpan.Zero).WithMessage("StartTime must be greater than zero")
                .Must((request, startTime) => startTime < request.EndTime)
                .WithMessage("Start time must be earlier than end time.");
            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("EndTime cannot be empty")
                .GreaterThan(TimeSpan.Zero).WithMessage("EndTime must be greater than zero")
                .Must((request, endTime) => endTime > request.StartTime)
                .WithMessage("End time must be later than start time.");
            RuleFor(x=>x.DayType)
                .NotEmpty().WithMessage("DayType cannot be empty")
                .Must(dayType => dayType == "Weekday" || dayType == "Weekend" || dayType == "Holiday")
                .WithMessage("Day type must be either 'Weekday', 'Weekend', or 'Holiday'.");

        }
    }
}
