using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.GetAll
{
    public class GetAllTransactionsQueryResponse
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? VehicleType { get; set; }

    }
}
