using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.DTOs
{
    public class ErrorLogDTO
    {
        public string Id { get; set; } = null!;
        public string ErrorMessage { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Method { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
