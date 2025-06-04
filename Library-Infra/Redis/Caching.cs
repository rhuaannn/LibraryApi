using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Library_Infra.Redis
{
    public class Caching : ICachingService
    {

        private readonly DistributedCacheEntryOptions _options;
           
        private readonly IDistributedCache _cache;
        public Caching(IDistributedCache cache, IOptions<CacheSettings> cacheSettings)
        {
            _cache = cache;
            var cacheSetting = cacheSettings.Value;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheSetting.AbsoluteExpirationRelativeToNow),
                SlidingExpiration = TimeSpan.FromMinutes(cacheSetting.SlidingExpiration)
            };

        }
        public async Task SetAsync(string key, string value)
        {
             await _cache.SetStringAsync(key, value, _options);
        }
        public async Task<string?> GetAsync(string key)
        {
             return await _cache.GetStringAsync(key);

        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }

}
