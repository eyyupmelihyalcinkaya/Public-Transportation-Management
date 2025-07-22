using MongoDB.Driver;
using internshipproject1.Domain.Entities;
using Microsoft.Extensions.Configuration;
using internshipproject1.Application.DTOs;

namespace internshipProject1.Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        
        public MongoDbContext(IMongoDatabase database)
        {
            _database = database;
        }
        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<ErrorLogDTO> ErrorLogs => 
            _database.GetCollection<ErrorLogDTO>("ErrorLogs");

        public IMongoCollection<Log> Log => _database.GetCollection<Log>("Logs");

        public IMongoDatabase Database => _database;

        public async Task<bool> PingAsync()
        {
            try
            {
                await _database.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
