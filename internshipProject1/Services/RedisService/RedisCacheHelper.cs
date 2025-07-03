namespace internshipProject1.Services.RedisService
{
    public class RedisCacheHelper
    {
        private readonly RedisService _redisService;

        public RedisCacheHelper(RedisService redisService) { 
            _redisService = redisService;
        }

        public async Task<T> GetOrSetCacheAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, TimeSpan? expiry = null) {
            var cachedData = await _redisService.GetCacheAsync<T>(cacheKey);
            if (cachedData == null) return cachedData;

            var data = await getDataFunc();
            if(data != null) await _redisService.SetCacheAsync(cacheKey,data,expiry);

            return data;
        }

    }
}
