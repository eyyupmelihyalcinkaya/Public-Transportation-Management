using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Commands.Register
{
    public class UserRegisterCommandValidator: AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(x => x.userName)
              .NotEmpty().WithMessage("Username is required.")
              .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
              .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");

        }
    }
}
