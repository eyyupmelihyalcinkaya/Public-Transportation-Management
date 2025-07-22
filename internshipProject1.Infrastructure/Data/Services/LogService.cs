using internshipproject1.Application.DTOs;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Application.Interfaces.Services;
using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipProject1.Infrastructure.Data.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository) 
        {
            _logRepository = logRepository;
        }

        public async Task<List<LogDTO>> GetAllAsync()
        {
            var logs = await _logRepository.GetAllAsync();
            
            var result = logs.Select(log => new LogDTO
            {
                Id = log.Id,
                Level = log.Level,
                Message = log.Message,
                RequestPath = log.RequestPath,
                RequestMethod = log.RequestMethod,
                StatusCode = log.StatusCode,
                CreatedAt = log.CreatedAt
            }).ToList();

            return result;
            
        }

        async public Task LogAsync(Log log)
        {
            await _logRepository.InsertAsync(log);
        }
    }
}
