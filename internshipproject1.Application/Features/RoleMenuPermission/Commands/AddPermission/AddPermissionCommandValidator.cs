using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RoleMenuPermission.Commands.AddPermission
{
    public class AddPermissionCommandValidator : AbstractValidator<AddPermissionCommandRequest>
    {
        public AddPermissionCommandValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.")
                .GreaterThan(0).WithMessage("RoleId must be greater than 0.");
            RuleFor(x => x.MenuId)
                .NotEmpty().WithMessage("MenuId is required.")
                .GreaterThan(0).WithMessage("MenuId must be greater than 0.");
            RuleFor(x => x.CanRead)
                .NotNull().WithMessage("CanRead is required.");
            RuleFor(x => x.CanCreate)
                .NotNull().WithMessage("CanCreate is required.");
            RuleFor(x => x.CanUpdate)
                .NotNull().WithMessage("CanUpdate is required.");
            RuleFor(x => x.CanDelete)
                .NotNull().WithMessage("CanDelete is required.");
        }
    }
}
