using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        private readonly ICardTransaction _cardTransaction;

        public GetByIdQueryHandler(ICardTransaction cardTransaction)
        {
            _cardTransaction = cardTransaction;
        }
        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        { 
            var cardTransaction = await _cardTransaction.GetByIdAsync(request.Id, cancellationToken);
            if (cardTransaction == null)
            {
                return new GetByIdQueryResponse {
                    Message = "Card Transaction not found"
                };
            }
            return new GetByIdQueryResponse
            {
                Message = "Card Transaction found",
                Id = cardTransaction.Id,
                CardId = cardTransaction.CardId,
                TransactionDate = cardTransaction.TransactionDate,
                Amount = cardTransaction.Amount,
                VehicleType = cardTransaction.VehicleType
            };
        }
    }
}
