using internshipproject1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Features.User.Queries.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public int id { get; set; }
        public string userName { get; set; } = string.Empty;
   //     public UserRole Role { get; set; }
    }
}
