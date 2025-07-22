using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Stop.Commands.DeleteStopCommand
{
    public class DeleteStopCommandRequest : IRequest<DeleteStopCommandResponse> 
    {
        public int Id { get; set; }

        public DeleteStopCommandRequest(int id)
        {
            Id = id;
        }
    }
}
