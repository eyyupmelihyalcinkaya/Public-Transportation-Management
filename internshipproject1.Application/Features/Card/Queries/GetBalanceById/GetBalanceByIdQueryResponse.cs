using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetBalanceById
{
    public class GetBalanceByIdQueryResponse
    {
        public int CardId { get; set; }
        public decimal Balance { get; set; }
    }
}
