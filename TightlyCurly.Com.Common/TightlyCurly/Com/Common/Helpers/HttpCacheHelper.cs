using System;
using Microsoft.Extensions.Caching.Memory;

namespace TightlyCurly.Com.Common.Helpers;

/// <summary>
/// In-memory cache helper. Formerly backed by HttpRuntime.Cache (System.Web),
/// now backed by Microsoft.Extensions.Caching.Memory.
/// </summary>
public class HttpCacheHelper : ICacheHelper
{
    private const double DefaultExpiration = 20.0;

    private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

    private readonly IConfigurationHelper _configurationHelper;

    public bool CacheEnabled => _configurationHelper.CacheEnabled;

    public HttpCacheHelper(IConfigurationHelper configurationHelper)
    {
        if (configurationHelper == null)
        {
            throw new ArgumentException("configurationHelper");
        }
        _configurationHelper = configurationHelper;
    }

    public object RetrieveFromCache(string objectName)
    {
        return Cache.TryGetValue(objectName, out object value) ? value : null;
    }

    public void InsertCache(string key, object value)
    {
        double expiration = _configurationHelper.DefaultCacheExpiration;
        if (expiration == 0.0)
        {
            expiration = DefaultExpiration;
        }
        InsertCache(key, value, expiration, TimeSpan.Zero);
    }

    public void InsertCache(string key, object value, double absoluteExpiration, TimeSpan slidingExpiration)
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

    public void RemoveFromCache(string key)
    {
        if (!string.IsNullOrEmpty(key) && IsInCache(key))
        {
            Cache.Remove(key);
        }
    }

    public bool IsInCache(string key)
    {
        return Cache.TryGetValue(key, out _);
    }
}
