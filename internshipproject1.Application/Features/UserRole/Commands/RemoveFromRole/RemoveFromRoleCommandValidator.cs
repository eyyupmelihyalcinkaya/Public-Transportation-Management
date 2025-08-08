using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Commands.RemoveFromRole
{
    public class RemoveFromRoleCommandValidator : AbstractValidator<RemoveFromRoleCommandRequest>
    {
        public RemoveFromRoleCommandValidator() 
        { 
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty.")
                .GreaterThan(0).WithMessage("User ID must be greater than zero.");
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID cannot be empty.")
                .GreaterThan(0).WithMessage("Role ID must be greater than zero.");
        }
    }
}
