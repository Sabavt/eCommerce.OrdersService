using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace eCommerce.Core.Redis;

public class RedisCacheService(IDistributedCache distributedCache)
{
    private readonly IDistributedCache _cache = distributedCache;

    private static readonly DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

    public async Task SetStringAsync<T>(string key, T value, CancellationToken token)
    {
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options, token);
    }
} 