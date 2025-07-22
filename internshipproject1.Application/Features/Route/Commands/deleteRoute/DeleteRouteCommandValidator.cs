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
            RuleFor(x=> x.Name)
                .NotEmpty()
                .WithMessage("Route name is required.")
                .MaximumLength(100)
                .WithMessage("Route name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Route description must not exceed 500 characters.");
            RuleFor(x=> x.StartLocation)
                .Length(0, 200)
                .WithMessage("Start location must not exceed 200 characters.");
            RuleFor(x=>x.EndLocation)
                .Length(0,200)
                .WithMessage("End location must not exceed 200 characters.");


        }
    }
}
