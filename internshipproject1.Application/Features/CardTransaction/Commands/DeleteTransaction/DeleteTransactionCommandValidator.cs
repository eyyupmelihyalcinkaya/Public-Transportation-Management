using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommandRequest>
    {
        public DeleteTransactionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Transaction ID is required.")
                .GreaterThan(0).WithMessage("Transaction ID must be greater than 0.");
        }
    }
}
