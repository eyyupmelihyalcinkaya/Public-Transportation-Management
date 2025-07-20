using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Commands.DeleteStopCommand
{
    public class DeleteStopCommandValidator : AbstractValidator<DeleteStopCommandRequest>
    {
        public DeleteStopCommandValidator()
        { 
            RuleFor(x=>x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

        }
    }
}
