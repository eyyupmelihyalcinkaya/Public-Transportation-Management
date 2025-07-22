using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.DeleteCard
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommandRequest>
    {
        public DeleteCardCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Card ID is required.")
                .GreaterThan(0).WithMessage("Card ID must be greater than zero.");
        }

    }
}
