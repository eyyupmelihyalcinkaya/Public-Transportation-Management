using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Domain.Exceptions
{
    public class UserAlreadyRegisteredException :Exception
    {
        public UserAlreadyRegisteredException(string? message) : base(message)
        { 
        }
    }
}
