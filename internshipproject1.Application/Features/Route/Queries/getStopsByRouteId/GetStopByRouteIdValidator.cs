using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Route.Queries.GetStopsByRouteId
{
    public class GetStopByRouteIdValidator : AbstractValidator<GetStopsByRouteIdRequest>
    {
        public GetStopByRouteIdValidator()
        {
            RuleFor(x => x.RouteId)
                .NotEmpty().WithMessage("RouteId cannot be empty.")
                .GreaterThan(0).WithMessage("RouteId must be greater than 0.");
        }
    }
}
