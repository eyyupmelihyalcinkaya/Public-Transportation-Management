using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.DecreaseBalance
{
    public class DecreaseBalanceCommandRequest : IRequest<DecreaseBalanceCommandResponse>
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public DecreaseBalanceCommandRequest(int cardId, decimal amount)
        {
            CardId = cardId;
            Amount = amount;
        }

    }
}
