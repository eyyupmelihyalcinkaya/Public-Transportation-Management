using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Queries.IsExists
{
    public class IsRoleExistsQueryRequest : IRequest<IsRoleExistsQueryResponse>
    {
        public int Id { get; set; }
        public IsRoleExistsQueryRequest(int id)
        {
            Id = id;
        }
    }
}
