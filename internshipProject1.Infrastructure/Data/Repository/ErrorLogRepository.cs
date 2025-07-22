using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using internshipproject1.Application.DTOs;
using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using internshipProject1.Infrastructure.Context;
using MongoDB.Driver;

namespace internshipProject1.Infrastructure.Data.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly MongoDbContext _mongoContext;
        private readonly IMongoCollection<ErrorLogDTO> _errorLogs;

        public ErrorLogRepository(MongoDbContext mongoContext)
        {
            _mongoContext = mongoContext;
            _errorLogs = (IMongoCollection<ErrorLogDTO>?)_mongoContext.ErrorLogs;
        }

        public async Task LogErrorAsync(ErrorLogDTO errorLog)
        {
            if (errorLog == null)
                throw new ArgumentNullException(nameof(errorLog));
            errorLog.CreatedAt = DateTime.UtcNow;
            
            await _errorLogs.InsertOneAsync(errorLog);
        }

        public async Task<List<ErrorLogDTO>> GetAllAsync()
        {
            return await _errorLogs
                .Find(_ => true)
                .SortByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<ErrorLogDTO?> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return await _errorLogs
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ErrorLogDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _errorLogs
                .Find(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate)
                .SortByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<ErrorLogDTO>> GetPaginatedAsync(int page, int pageSize)
        {
            return await _errorLogs
                .Find(_ => true)
                .SortByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }
    }
}
