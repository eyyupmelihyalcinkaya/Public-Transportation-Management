using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.UpdateBalance
{
    public class UpdateBalanceCommandResponse
    {
        public int CardId { get; set; }
        public decimal NewBalance { get; set; }
        public bool IsIncrease { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }

    }
}
