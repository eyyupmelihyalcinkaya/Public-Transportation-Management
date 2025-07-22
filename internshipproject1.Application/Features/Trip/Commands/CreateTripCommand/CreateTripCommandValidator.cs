using FluentValidation;
using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Commands.CreateTripCommand
{
    public class CreateTripCommandValidator : AbstractValidator<CreateTripCommandRequest>
    {
        private readonly IRouteRepository _routeRepository;

        public CreateTripCommandValidator(IRouteRepository routeRepository)
        { 
            _routeRepository = routeRepository;

            RuleFor(x => x.RouteId)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _routeRepository.RouteExistByIdAsync(id, cancellationToken);
                })
                .WithMessage("Route does not exist.")
                .GreaterThan(0).WithMessage("Route ID must be greater than 0.")
                .NotEmpty().WithMessage("Route ID cannot be empty.");

            RuleFor(x=>x.StartTime)
                .NotEmpty().WithMessage("Start time cannot be empty.")
                .Must((request, startTime) => startTime < request.EndTime)
                .WithMessage("Start time must be earlier than end time.");
            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time cannot be empty.")
                .Must((request, endTime) => endTime > request.StartTime)
                .WithMessage("End time must be later than start time.");
            RuleFor(x => x.DayType)
                .NotEmpty().WithMessage("Day type cannot be empty.")
                .WithMessage("Day type must be either 'Weekday', 'Weekend', or 'Holiday'.");
        }
    }
}
