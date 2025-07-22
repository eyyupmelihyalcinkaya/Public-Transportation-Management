using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.DTOs
{
    public class ErrorLogDetailDTO
    {
        public string Id { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? StackTrace { get; set; }
        public string Path { get; set; } = null!;
        public string QueryString { get; set; } = null!;
        public string Method { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
