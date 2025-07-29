using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.UpdateTransaction
{
    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommandRequest>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Transaction Id is required.");
            RuleFor(x => x.CardId).NotEmpty().WithMessage("Card Id is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.TransactionDate).NotEmpty().WithMessage("Transaction date is required.");
            RuleFor(x => x.VehicleType).NotEmpty().WithMessage("Vehicle type is required.");
        }
    }
}
