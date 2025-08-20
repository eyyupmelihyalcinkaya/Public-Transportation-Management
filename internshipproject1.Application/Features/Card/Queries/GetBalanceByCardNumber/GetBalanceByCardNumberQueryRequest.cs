using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetBalanceByCardNumber
{
    public class GetBalanceByCardNumberQueryRequest : IRequest<GetBalanceByCardNumberQueryResponse>
    {
        public string CardNumber { get; set; }
        public GetBalanceByCardNumberQueryRequest(string cardNumber)
        {
            CardNumber = cardNumber;
        }
        
    }
}
