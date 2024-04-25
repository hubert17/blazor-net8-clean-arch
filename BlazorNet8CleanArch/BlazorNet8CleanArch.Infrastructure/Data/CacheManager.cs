using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BlazorNet8CleanArch.Infrastructure.Data;

public static class CacheManager
{
    private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private static readonly SemaphoreSlim _lockObject = new SemaphoreSlim(1, 1);

    public static async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getItemCallback, DateTimeOffset absoluteExpiration)
    {
        if (!_cache.TryGetValue(key, out T? cachedItem))
        {
            await _lockObject.WaitAsync();
            try
            {
                if (!_cache.TryGetValue(key, out cachedItem))
                {
                    cachedItem = await getItemCallback();
                    if (cachedItem != null)  // Only cache if the item is not null
                        _cache.Set(key, cachedItem, absoluteExpiration);
                }
            }
            finally
            {
                _lockObject.Release();
            }
        }
        return cachedItem!;
    }

    public static void Remove(string key)
    {
        _cache.Remove(key);
    }
}