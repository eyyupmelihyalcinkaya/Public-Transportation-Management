using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.CardTransaction.Queries.GetById
{
    public class GetByIdQueryRequest : IRequest<GetByIdQueryResponse>
    {
        public int Id { get; set; }
        public GetByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
