using internshipproject1.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Cache
{
    public class RedisCacheService : IRedisCacheService
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
        // Kullanacağım ana metot
        public async Task<T> GetOrSetCacheAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, TimeSpan? expiry = null)
        {
            var cachedData = await GetCacheAsync<T>(cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }

            var data = await getDataFunc();
            if (data != null)
            {
                await SetCacheAsync(cacheKey, data, expiry);
            }

            return data;
        }
    }
}
