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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            };

        }
        public async Task SetAsync(string key, string value)
        {
             await _cache.SetStringAsync(key, value, _options);
        }
        public async Task<string> GetAsync(string key)
        {
             return await _cache.GetStringAsync(key);

        }

        public async Task RemoveAsyc(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }

}
