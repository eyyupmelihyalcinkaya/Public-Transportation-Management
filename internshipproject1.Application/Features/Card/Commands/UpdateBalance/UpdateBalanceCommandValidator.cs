using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateBalance
{
    public class UpdateBalanceCommandValidator : AbstractValidator<UpdateBalanceCommandRequest>
    {
        public UpdateBalanceCommandValidator()
        { 
            RuleFor(x=>x.CardId)
                .NotEmpty().WithMessage("Card ID cannot be empty.")
                .GreaterThan(0).WithMessage("Card ID must be greater than zero.");
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount cannot be empty.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}
