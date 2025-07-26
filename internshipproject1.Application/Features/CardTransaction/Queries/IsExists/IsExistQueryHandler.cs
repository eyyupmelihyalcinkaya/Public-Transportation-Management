using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.IsExists
{
    public class IsExistQueryHandler : IRequestHandler<IsExistQueryRequest, IsExistQueryResponse>
    {
        private readonly ICardTransaction _cardTransactionRepository;
        public IsExistQueryHandler(ICardTransaction cardTransactionRepository)
        {
            _cardTransactionRepository = cardTransactionRepository;
        }
        public async Task<IsExistQueryResponse> Handle(IsExistQueryRequest request, CancellationToken cancellationToken)
        {
            var exists = await _cardTransactionRepository.CardTransactionExistsAsync(request.Id, cancellationToken);
            return new IsExistQueryResponse
            {
                IsExists = exists,
                Message = exists ? "Card transaction exists." : "Card transaction does not exist."
            };
        }
    }
}
