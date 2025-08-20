using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace internshipproject1.Application.Features.Role.Commands.AddRole
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommandRequest>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Role description cannot exceed 500 characters.");
    }
}
}
