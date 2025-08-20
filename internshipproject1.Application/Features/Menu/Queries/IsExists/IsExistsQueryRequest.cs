using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Menu.Queries.IsExists
{
    public class IsExistsQueryRequest : IRequest<IsExistsQueryResponse>
    {
        public int Id { get; set; }
        public IsExistsQueryRequest(int id)
        {
            Id = id;
        }
    }
}
