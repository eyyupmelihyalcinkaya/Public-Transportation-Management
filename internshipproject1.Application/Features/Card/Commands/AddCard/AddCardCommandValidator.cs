using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.AddCard
{
    public class AddCardCommandValidator : AbstractValidator<AddCardCommandRequest>
    {
        public AddCardCommandValidator() 
        {
            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("Customer Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .WithName("Customer Email");
            RuleFor(x => x.Balance)
                .NotNull().WithMessage("Balance is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to 0.")
                .WithName("Balance");
            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("Expiration date is required.")
                .GreaterThan(DateTime.Now).WithMessage("Expiration date must be in the future.")
                .WithName("Expiration Date");
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive status is required.")
                .WithName("Is Active");
            RuleFor(x => x.IsDeleted)
                .NotNull().WithMessage("IsDeleted status is required.")
                .WithName("Is Deleted");
        }
    }
}
