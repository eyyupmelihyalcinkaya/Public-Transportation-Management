using internshipproject1.Application.DTOs;
using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Services
{
    public interface ILogService
    {
        public Task LogAsync(Log log);
        public Task<List<LogDTO>> GetAllAsync();
    }
}
