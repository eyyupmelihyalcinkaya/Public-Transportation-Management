using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Commands.CreateStopCommand
{
    public class CreateStopCommandValidator : AbstractValidator<CreateStopCommandRequest>
    {
        public CreateStopCommandValidator()
        {
            RuleFor(x => x.StopName)
                .NotEmpty().WithMessage("Stop name is required.")
                .MaximumLength(100).WithMessage("Stop name must not exceed 100 characters.");
            RuleFor(x => x.Latitude)
                .NotEmpty().WithMessage("Latitude is required.")
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90 degrees.");
            RuleFor(x => x.Longitude)
                .NotEmpty().WithMessage("Longitude is required.")
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180 degrees.");
        }
    }
}
