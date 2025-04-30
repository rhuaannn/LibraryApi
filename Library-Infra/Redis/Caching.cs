using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Library_Infra.Redis
{
    public class Caching : ICachingService
    {

        private readonly DistributedCacheEntryOptions _options;
           
        private readonly IDistributedCache _cache;
        public Caching(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(120),
                SlidingExpiration = TimeSpan.FromMinutes(120)
            };

        }
        public async Task SetAsync(string key, string value)
        {
             await _cache.SetStringAsync(key, value, _options);
        }
        public async Task<string> GetAsync<T>(string key)
        {
            return await _cache.GetStringAsync(key);
        }
    }
    
}
