using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Commands.CreateRoute
{
    public class CreateRouteCommandValidator : AbstractValidator<CreateRouteCommand>
    {
        public CreateRouteCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Route name is required.")
                .WithName("Name");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Route description is required.")
                .WithName("Description")
                .MaximumLength(250).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x => x.StartLocation)
                .NotEmpty().WithMessage("Start location is required.")
                .WithName("Start Location")
                .MaximumLength(100).WithMessage("Start location cannot exceed 100 characters.");
            RuleFor(x => x.EndLocation)
                .NotEmpty().WithMessage("End location is required.")
                .WithName("End Location")
                .MaximumLength(100).WithMessage("End location cannot exceed 100 characters.");

        }
    }
}
