using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Trip.Commands.DeleteTripCommand
{
    public class DeleteTripCommandRequest : IRequest<DeleteTripCommandResponse>
    {
        public int Id { get; set; }

        public DeleteTripCommandRequest(int id)
        {
            Id = id;
        }
    }
}
