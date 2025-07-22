using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.RouteStop.Queries.GetRouteStopById
{
    public class GetRouteStopByIdQueryValidator : AbstractValidator<GetRouteStopByIdQueryRequest>
    {
        public GetRouteStopByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("RouteStop Id cannot be empty.")
                .GreaterThan(0).WithMessage("RouteStop Id must be greater than zero.");


        }
    }
}
