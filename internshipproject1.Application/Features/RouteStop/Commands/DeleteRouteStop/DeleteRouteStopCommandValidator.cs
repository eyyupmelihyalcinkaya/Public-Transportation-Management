using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Commands.DeleteRouteStop
{
    public class DeleteRouteStopCommandValidator : AbstractValidator<DeleteRouteStopCommandRequest>
    {
        public DeleteRouteStopCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be empty.")
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}
