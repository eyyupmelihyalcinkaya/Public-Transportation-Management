using internshipproject1.Application.Interfaces.Repositories;
using internshipproject1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using internshipProject1.Infrastructure.Context;
using internshipproject1.Application.DTOs;
namespace internshipProject1.Infrastructure.Data.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<Log> _logCollection;

        public LogRepository(MongoDbContext context)
        {
            _logCollection = context.Database.GetCollection<Log>("Logs");
        }

        public async Task InsertAsync(Log log)
        {
            await _logCollection.InsertOneAsync(log);
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return await _logCollection.Find(_ => true).ToListAsync();
            
        }
    }
}
