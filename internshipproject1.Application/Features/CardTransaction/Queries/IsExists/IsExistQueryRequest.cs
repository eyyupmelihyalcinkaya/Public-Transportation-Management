using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.IsExists
{
    public class IsExistQueryRequest : IRequest<IsExistQueryResponse>
    {
        public int Id { get; set; }
        public IsExistQueryRequest(int id)
        {
            Id = id;
        }
    }
}
