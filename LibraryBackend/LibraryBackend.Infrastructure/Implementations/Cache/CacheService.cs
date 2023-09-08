using System.Text;
using LibraryBackend.Application.Interfaces.Infrastructure.Cache;
using LibraryBackend.Application.Utilities.Configurations;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LibraryBackend.Infrastructure.Implementations.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _redisCache;
    private readonly CacheSetting _cacheSetting;

    public CacheService(IDistributedCache redisCache,
        IOptionsMonitor<CacheSetting> cacheOptionsConfig)
    {
        _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        _cacheSetting = cacheOptionsConfig.CurrentValue;
    }
    
    public async Task<T?> GetCacheData<T>(string key)
    {
        var data = await _redisCache.GetStringAsync(key);

        if (!string.IsNullOrEmpty(data))
            return JsonConvert.DeserializeObject<T>(data);
        return default;
    }

    public async Task SetCacheData<T>(string key, T value)
    {
        string cachedObjectJson = await _redisCache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedObjectJson))
        {
            await RemoveCache(key);
        }
        try
        {
            string catchdata = JsonConvert.SerializeObject(value);
            byte[] redisData = Encoding.UTF8.GetBytes(catchdata);

            string cacheKey = $"{key}";

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(_cacheSetting.AbsoluteExpirationInHours))
                .SetSlidingExpiration(TimeSpan.FromMinutes(_cacheSetting.SlidingExpirationInMinutes));
            await _redisCache.SetAsync(cacheKey, redisData, options);
            
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Error in CacheService.CS :- " + ex.Message, ex.InnerException);
        }
    }
    public async Task RemoveCache(string key)
    {
        await _redisCache.RemoveAsync(key);
    }
}