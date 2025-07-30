using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.GetAll
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQueryRequest, IReadOnlyList<GetAllTransactionsQueryResponse>>
    {
        private readonly ICardTransaction _cardTransaction;
        public GetAllTransactionsQueryHandler(ICardTransaction cardTransaction)
        {
            _cardTransaction = cardTransaction;
        }
        public async Task<IReadOnlyList<GetAllTransactionsQueryResponse>> Handle(GetAllTransactionsQueryRequest request, CancellationToken cancellationToken)
        { 
            var transactions = await _cardTransaction.GetAllAsync(cancellationToken);
            var response = transactions.Select(t => new GetAllTransactionsQueryResponse
            {
                Id = t.Id,
                CardId = t.CardId,
                Amount = t.Amount,
                TransactionType = t.TransactionType,
                TransactionDate = t.TransactionDate,
                VehicleType = t.VehicleType
            }).ToList();
            return response;
        }
    }
}
