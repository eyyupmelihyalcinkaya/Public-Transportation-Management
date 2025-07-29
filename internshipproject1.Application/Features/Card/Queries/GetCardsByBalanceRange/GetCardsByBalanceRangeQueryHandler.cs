using internshipproject1.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardsByBalanceRange
{
    public class GetCardsByBalanceRangeQueryHandler : IRequestHandler<GetCardsByBalanceRangeQueryRequest,IEnumerable<GetCardsByBalanceRangeQueryResponse>>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICustomerRepository _customerRepository;
        public GetCardsByBalanceRangeQueryHandler(ICardRepository cardRepository, ICustomerRepository customerRepository)
        {
            _cardRepository = cardRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<GetCardsByBalanceRangeQueryResponse>> Handle(GetCardsByBalanceRangeQueryRequest request, CancellationToken cancellationToken)
        { 
            var cards = await _cardRepository.GetCardsByBalanceRangeAsync(request.MinBalance, request.MaxBalance,cancellationToken);

            var response = cards.Select(c=> new GetCardsByBalanceRangeQueryResponse
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                Balance = c.Balance
            });
            return response;
        }
    }
}
