using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Commands.CreateRouteWithStops
{
    public class CreateRouteWithStopsCommandValidator : AbstractValidator<CreateRouteWithStopsCommandRequest>
    {
        public CreateRouteWithStopsCommandValidator() 
        {
            RuleFor(x => x.RouteName)
            .NotEmpty().WithMessage("Route name is required.")
            .MaximumLength(100).WithMessage("Route name cannot exceed 100 characters.");

            RuleFor(x => x.CreatedById)
                .GreaterThan(0).WithMessage("CreatedBy ID must be greater than 0.");

            RuleFor(x => x.Stops)
                .NotEmpty().WithMessage("At least one stop is required.");

            RuleForEach(x => x.Stops).ChildRules(stop =>
            {
                stop.RuleFor(x => x.StopId).GreaterThanOrEqualTo(0).WithMessage("Stop ID must be greater than 0.");
                stop.RuleFor(x => x.Order).GreaterThan(0).WithMessage("Order must be greater than 0.");
            });
        }
    }
}
