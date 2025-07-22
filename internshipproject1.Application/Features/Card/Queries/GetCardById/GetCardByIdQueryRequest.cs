using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Card.Queries.GetCardById
{
    public class GetCardByIdQueryRequest : IRequest<GetCardByIdQueryResponse>
    {
        public int Id { get; set; }
        public GetCardByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
