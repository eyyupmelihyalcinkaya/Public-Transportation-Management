using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommandRequest>
    {
        public CreateCustomerCommandValidator() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
            RuleFor(c => c.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(50).WithMessage("Surname cannot exceed 50 characters.");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
        }
    }
}
