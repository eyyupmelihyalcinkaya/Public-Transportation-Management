using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandRequest : IRequest<DeleteTransactionCommandResponse>
    {
        public int Id { get; set; }
        public DeleteTransactionCommandRequest(int id)
        {
            Id = id;
        }
        
    }
}
