using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.UserRole.Queries.GetAll
{
    public class GetAllUserRolesQueryRequest : IRequest<List<GetAllUserRolesQueryResponse>>
    {
    }
}
