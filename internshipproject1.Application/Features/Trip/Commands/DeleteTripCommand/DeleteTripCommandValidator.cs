using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Commands.DeleteTripCommand
{
    public class DeleteTripCommandValidator : AbstractValidator<DeleteTripCommandRequest>
    {
        public DeleteTripCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty")
            .GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.Id)
                .IsInEnum().WithMessage("Id must be a valid integer value");
        }

    }
}
