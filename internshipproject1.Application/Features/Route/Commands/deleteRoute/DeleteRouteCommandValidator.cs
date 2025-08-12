using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Commands.DeleteRoute
{
    public class DeleteRouteCommandValidator : AbstractValidator<DeleteRouteCommandRequest>
    {
        public DeleteRouteCommandValidator()
        { 
            RuleFor(x=>x.Id)
                .NotEmpty()
                .WithMessage("Route ID is required.")
                .GreaterThan(0)
                .WithMessage("Route ID must be greater than zero.");
        }
    }
}
