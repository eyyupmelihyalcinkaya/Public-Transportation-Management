using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandResponse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        
    }
}
