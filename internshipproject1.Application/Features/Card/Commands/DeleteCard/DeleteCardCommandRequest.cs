using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Commands.DeleteCard
{
    public class DeleteCardCommandRequest : IRequest<DeleteCardCommandResponse>
    {
        public int Id { get; set; }
        public DeleteCardCommandRequest(int id)
        {
            Id = id;
        }
    }
}
