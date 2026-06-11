using System;
using System.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace TightlyCurly.Com.Framework.Web.Caching;

public static class CacheHelper
{
    private const double DefaultExpiration = 20.0;

    // Replaces HttpRuntime.Cache (System.Web), which does not exist on modern .NET.
    private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

    // Replaces System.Web.Caching.Cache.NoSlidingExpiration.
    public static readonly TimeSpan NoSlidingExpiration = TimeSpan.Zero;

    public static object RetrieveFromCache(string objectName)
    {
        return Cache.TryGetValue(objectName, out object value) ? value : null;
    }

    public static void InsertCache(string key, object value)
    {
        if (!double.TryParse(ConfigurationManager.AppSettings["DefaultCacheExpiration"], out var result))
        {
            result = DefaultExpiration;
        }
        InsertCache(key, value, result, NoSlidingExpiration);
    }

    public static void InsertCache(string key, object value, double absoluteExpiration, TimeSpan slidingExpiration)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(absoluteExpiration)
        };
        if (slidingExpiration > TimeSpan.Zero)
        {
            options.SlidingExpiration = slidingExpiration;
        }
        Cache.Set(key, value, options);
    }

    public static void RemoveFromCache(string key)
    {
        if (!string.IsNullOrEmpty(key))
        {
            Cache.Remove(key);
        }
    }

    public static bool IsInCache(string key)
    {
        return Cache.TryGetValue(key, out _);
    }
}
