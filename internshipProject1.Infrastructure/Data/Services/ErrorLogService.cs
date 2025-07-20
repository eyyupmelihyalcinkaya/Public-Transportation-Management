using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.DTOs;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Application.Interfaces.Services;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Data.Repository;
using MongoDB.Driver;

namespace internshipProject1.Infrastructure.Data.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository _repository;

        public ErrorLogService(IErrorLogRepository repository)
        {
            _repository = repository;
        }

        public async Task LogErrorAsync(ErrorLogDTO errorLog)
        {
            await _repository.LogErrorAsync(errorLog);
        }

        public async Task<List<ErrorLogDTO>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<ErrorLogDTO?> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);

        }
    }
}
