using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace internshipproject1.Application.Features.Card.Queries.GetAllActiveCards
{
    public class GetAllActiveCardsQueryRequest : IRequest<List<GetAllActiveCardsQueryResponse>>
    {
    }
}
