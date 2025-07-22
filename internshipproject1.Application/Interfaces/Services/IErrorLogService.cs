using internshipproject1.Application.DTOs;
using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Services
{
    public interface IErrorLogService
    {
        Task LogErrorAsync(ErrorLogDTO errorLog);

        Task<List<ErrorLogDTO>> GetAllAsync();
        Task<ErrorLogDTO?> GetByIdAsync(string id);
    }
}
