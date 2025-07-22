using FluentValidation;
using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Commands.UpdateRouteStop
{
    public class UpdateRouteStopCommandValidator : AbstractValidator<UpdateRouteStopCommandRequest>
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IStopRepository _stopRepository;   
        public UpdateRouteStopCommandValidator (IRouteRepository routeRepository,IStopRepository stopRepository)
        {
            _routeRepository = routeRepository;
            _stopRepository =  stopRepository;

            RuleFor(x=>x.RouteId)
                .NotEmpty().WithMessage("RouteId cannot be empty")
                .GreaterThan(0).WithMessage("RouteId must be greater than 0");
            RuleFor(x => x.RouteId)
                .MustAsync(async (routeId, cancellationToken) =>
                {
                    return await _routeRepository.RouteExistByIdAsync(routeId, cancellationToken);
                }).WithMessage("Route with given RouteId does not exist");
            RuleFor(x => x.StopId)
                .MustAsync(async (stopId, cancellationToken) =>
                {
                    return await _stopRepository.StopExistsByIdAsync(stopId, cancellationToken);
                });
            RuleFor(x=>x.StopId)
                .NotEmpty().WithMessage("StopId cannot be empty")
                .GreaterThan(0).WithMessage("StopId must be greater than 0");
            RuleFor(x => x.Order)
                .NotEmpty().WithMessage("Order cannot be empty")
                .GreaterThan(0).WithMessage("Order must be greater than 0");

        }
    }
}
