using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.IncreaseBalance
{
    public class IncreaseBalanceCommandValidator : AbstractValidator<IncreaseBalanceCommandRequest>
    {
        public IncreaseBalanceCommandValidator()
        { 
            RuleFor(c=>c.CardId)
                .GreaterThan(0).WithMessage("Card ID must be greater than zero.");
            RuleFor(c => c.Amount)
                .NotNull().WithMessage("Amount cannot be null.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
