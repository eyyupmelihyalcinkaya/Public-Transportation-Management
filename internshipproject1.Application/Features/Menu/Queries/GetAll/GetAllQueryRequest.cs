using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Queries.GetAll
{
    public class GetAllQueryRequest : IRequest<List<GetAllQueryResponse>>
    {
    }
}
