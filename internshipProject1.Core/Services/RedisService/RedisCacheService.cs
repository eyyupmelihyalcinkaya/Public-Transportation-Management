using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Core.Services.RedisService
{
    public class RedisCacheService
    {
        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _redisConnection;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redisConnection = connectionMultiplexer;
            _database = connectionMultiplexer.GetDatabase();
        }

        // Veri kaydetme
        public async Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, jsonData, expiry);
        }

        // Veri getirme
        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var jsonData = await _database.StringGetAsync(key);
            if (jsonData.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }

        //Cache temizleme
        public async Task Clear(string key) { 
            await _database.KeyDeleteAsync(key);
        }

        public void ClearAll() {
            var redisEndPoints = _redisConnection.GetEndPoints(true);
            foreach (var redisEndPoint in redisEndPoints) { 
                var redisServer = _redisConnection.GetServer(redisEndPoint);
                redisServer.FlushAllDatabases();
            }
        }
    }
}
