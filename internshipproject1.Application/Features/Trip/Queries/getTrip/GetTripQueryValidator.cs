using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Trip.Queries.GetTrip
{
    public class GetTripQueryValidator : AbstractValidator<GetTripQueryRequest>
    {
        public GetTripQueryValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Trip ID cannot be empty.")
                .GreaterThan(0).WithMessage("Trip ID must be greater than zero.")
                .IsInEnum().WithMessage("Trip ID must be a valid integer.");


        }
    }
}
