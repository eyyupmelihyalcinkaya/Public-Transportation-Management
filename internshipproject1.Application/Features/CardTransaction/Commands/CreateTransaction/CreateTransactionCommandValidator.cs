using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommandRequest>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.CardId)
                .NotEmpty().WithMessage("Card ID is required.")
                .GreaterThan(0).WithMessage("Card ID must be greater than 0.");
            RuleFor(x => x.TransactionDate)
                .NotEmpty().WithMessage("Transaction date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Transaction date cannot be in the future.");
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is required.")
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");
            RuleFor(x => x.VehicleType)
                .MaximumLength(50).WithMessage("Vehicle type cannot exceed 50 characters.");
        }
    }
}
