using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.DTOs
{
    public class LogDTO
    {
        public string Id { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string RequestPath { get; set; }
        public string RequestMethod { get; set; }
        public int StatusCode { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
