using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces
{
    public interface IRedisCacheService
    {
        public Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null);
        public Task<T?> GetCacheAsync<T>(string key);
        public Task Clear(string key);
        public void ClearAll();
        public Task<T> GetOrSetCacheAsync<T>(string cacheKey, Func<Task<T>> getDataFunc, TimeSpan? expiry = null);

    }
}
