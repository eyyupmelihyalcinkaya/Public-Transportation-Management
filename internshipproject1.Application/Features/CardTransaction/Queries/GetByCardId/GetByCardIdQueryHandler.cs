using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.GetByCardId
{
    public class GetByCardIdQueryHandler : IRequestHandler<GetByCardIdQueryRequest, GetByCardIdQueryResponse>
    {
        private readonly ICardTransaction _cardTransaction;
        public GetByCardIdQueryHandler(ICardTransaction cardTransaction)
        {
            _cardTransaction = cardTransaction;
        }
        public async Task<GetByCardIdQueryResponse> Handle(GetByCardIdQueryRequest request, CancellationToken cancellationToken)
        {
            var cardTransaction = await _cardTransaction.GetByCardIdAsync(request.CardId, cancellationToken);
            if (cardTransaction == null)
            {
                return new GetByCardIdQueryResponse { 
                    Message = "Card transaction not found."
                };
            }
            return new GetByCardIdQueryResponse
            {
                Message = "Card transaction found.",
                Id = cardTransaction.Id,
                CardId = cardTransaction.CardId,
                TransactionDate = cardTransaction.TransactionDate,
                Amount = cardTransaction.Amount,
                VehicleType = cardTransaction.VehicleType
            };
        }
    }
}
