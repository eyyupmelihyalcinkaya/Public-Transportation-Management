using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommandRequest>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.")
                .GreaterThan(0).WithMessage("Customer ID must be a positive integer.");
        }
    }
}
