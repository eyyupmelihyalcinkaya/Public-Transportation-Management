using FluentValidation;
using internshipproject1.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Stop.Queries.GetStopById
{
    public class GetStopByIdQueryValidator : AbstractValidator<GetStopByIdQueryRequest>
    {
        private readonly IStopRepository _stopRepository;
        public GetStopByIdQueryValidator(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Stop Id cannot be empty.")
                .GreaterThan(0).WithMessage("Stop Id must be greater than zero.");
            RuleFor(x => x.Id)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _stopRepository.StopExistsByIdAsync(id, cancellationToken);
                })
                .WithMessage("Stop with the given Id does not exist.");


        }
    }
}
