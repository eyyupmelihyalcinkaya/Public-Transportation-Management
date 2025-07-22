using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Exceptions
{
    public class WrongUsernameOrPasswordException : Exception
    {
        public WrongUsernameOrPasswordException(string? message) : base(message)
        {
            
        }
    }
    
}
