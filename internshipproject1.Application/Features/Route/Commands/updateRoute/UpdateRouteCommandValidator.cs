using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Commands.UpdateRoute
{
    public class UpdateRouteCommandValidator : AbstractValidator<UpdateRouteCommandRequest>
    {
        public UpdateRouteCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.StartLocation)
                .NotEmpty().WithMessage("Start Location is required.")
                .MaximumLength(200).WithMessage("Start Location must not exceed 200 characters.");
            RuleFor(x => x.EndLocation)
                .NotEmpty().WithMessage("End Location is required.")
                .MaximumLength(200).WithMessage("End Location must not exceed 200 characters.");
        }
    }
}
