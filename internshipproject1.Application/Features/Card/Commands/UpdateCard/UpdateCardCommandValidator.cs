using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateCard
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommandRequest>
    {
        public UpdateCardCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot Empty")
                .GreaterThanOrEqualTo(0).WithMessage("Id Must be Greater or Equal to 0");

            RuleFor(x => x.Balance)
                .NotEmpty().WithMessage("Balance cannot be Empty")
                .GreaterThanOrEqualTo(0).WithMessage("Balance cannot be smaller than zero (0)");
            RuleFor(x => x.ExpirationDate)
                .NotEmpty().WithMessage("ExpirationDate Cannot be Empty")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("ExpirationDate cannot be older than now");
        }
    }
}
