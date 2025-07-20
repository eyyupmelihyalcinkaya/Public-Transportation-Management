using FluentValidation;
using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Commands.AddRouteStop
{
    public class AddRouteStopCommandValidator : AbstractValidator<AddRouteStopCommandRequest>
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IStopRepository _stopRepository;
        public AddRouteStopCommandValidator(IRouteRepository routeRepository,IStopRepository stopRepository)
        { 
            _routeRepository = routeRepository;
            _stopRepository = stopRepository;

            RuleFor(x=>x.RouteId)
                .NotEmpty().WithMessage("RouteID cannot be empty.")
                .GreaterThan(0).WithMessage("RouteID must be greater than 0.");
           RuleFor(x => x.RouteId)
                .MustAsync(async (routeId, cancellation) =>
                {
                    return await _routeRepository.RouteExistByIdAsync(routeId, cancellation);
                }).WithMessage("Route with given RouteId does not exist");
            RuleFor(x => x.StopId)
                .MustAsync(async (stopId, cancellation) =>
                {
                    return await _stopRepository.StopExistsByIdAsync(stopId, cancellation);
                })
                .WithMessage("Stop with given StopId does not exist");
            RuleFor(x=>x.StopId)
                .NotEmpty().WithMessage("StopID cannot be empty.")
                .GreaterThan(0).WithMessage("StopID must be greater than 0.");
            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order cannot be empty.")
                .GreaterThan(0).WithMessage("Order must be greater than 0.");
        }
    }
}
