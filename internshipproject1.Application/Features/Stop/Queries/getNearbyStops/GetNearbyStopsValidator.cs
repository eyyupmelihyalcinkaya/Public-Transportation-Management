using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Queries.GetNearbyStops
{
    public class GetNearbyStopsValidator : AbstractValidator<GetNearbyStopsRequest>
    {
        public GetNearbyStopsValidator()
        { 
            RuleFor(x=> x.Latitude)
                .NotEmpty().WithMessage("Latitude cannot be empty.")
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90 degrees.");
            RuleFor(x=> x.Longitude)
                .NotEmpty().WithMessage("Longitude cannot be empty.")
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180 degrees.");    
        }
    }
}
