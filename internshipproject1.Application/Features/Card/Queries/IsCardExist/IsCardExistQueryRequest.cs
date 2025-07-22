using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Card.Queries.IsCardExist
{
    public class IsCardExistQueryRequest : IRequest<IsCardExistQueryResponse>
    {
        public int Id { get; set; }
        public IsCardExistQueryRequest(int id)
        {
            Id = id;
        }
    }
}
