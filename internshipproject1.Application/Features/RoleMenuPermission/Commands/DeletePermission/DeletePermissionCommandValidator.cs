using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.DeletePermission
{
    public class DeletePermissionCommandValidator : AbstractValidator<DeletePermissionCommandRequest>
    {
        public DeletePermissionCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Role ID is required.")
                .GreaterThan(0).WithMessage("Role ID must be greater than 0.");
            RuleFor(x => x.MenuId)
                .NotEmpty().WithMessage("Menu ID is required.")
                .GreaterThan(0).WithMessage("Menu ID must be greater than 0.");
        }
    }
}
