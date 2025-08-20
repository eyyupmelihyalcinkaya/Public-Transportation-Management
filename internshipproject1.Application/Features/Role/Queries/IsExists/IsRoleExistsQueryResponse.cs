using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.Role.Queries.IsExists
{
    public class IsRoleExistsQueryResponse
    {
        public bool IsExists { get; set; }
        public IsRoleExistsQueryResponse(bool isExists)
        {
            IsExists = isExists;
        }
    }
}
