using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("Username is required.")
               .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
               .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
               .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers and underscores.");

            RuleFor(x => x.oldPassword)
                .NotEmpty().WithMessage("Old password is required.")
                .MinimumLength(6).WithMessage("Old password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Old password must not exceed 100 characters.");

            RuleFor(x => x)
                .Must(x => x.oldPassword != x.newPassword)
                .WithMessage("New password cannot be the same as the old password.");
        }
    }
}
