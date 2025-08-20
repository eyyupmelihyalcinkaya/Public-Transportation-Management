using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetBalanceByCardNumber
{
    public class GetBalanceByCardNumberQueryHandler : IRequestHandler<GetBalanceByCardNumberQueryRequest,GetBalanceByCardNumberQueryResponse>
    {
        private readonly ICardRepository _cardRepository;

        public GetBalanceByCardNumberQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<GetBalanceByCardNumberQueryResponse> Handle(GetBalanceByCardNumberQueryRequest request, CancellationToken cancellationToken)
        {
            var balance = await _cardRepository.GetBalanceByCardNumberAsync(request.CardNumber,cancellationToken);
            if (balance < 0 || balance == null)
            {
                throw new Exception("Balance bulunamadı !");
            }
            return new GetBalanceByCardNumberQueryResponse
            {
                Balance = balance,
                CardNumber = request.CardNumber
            };
        }
    }
}
