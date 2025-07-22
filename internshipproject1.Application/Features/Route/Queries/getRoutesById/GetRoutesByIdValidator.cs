using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Queries.GetRoutesById
{
    public class GetRoutesByIdValidator : AbstractValidator<GetRoutesByIdRequest>
    {
        public GetRoutesByIdValidator()
        { 
            RuleFor(x=> x.Id)
                .NotEmpty()
                .WithName("Id")
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");
        }
    }
}
