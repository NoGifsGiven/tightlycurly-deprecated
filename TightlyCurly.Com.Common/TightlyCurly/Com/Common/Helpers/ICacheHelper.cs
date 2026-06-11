using System;

namespace TightlyCurly.Com.Common.Helpers;

public interface ICacheHelper
{
    bool CacheEnabled { get; }

    object RetrieveFromCache(string objectName);

    void InsertCache(string key, object value);

    void InsertCache(string key, object value, double absoluteExpiration, TimeSpan slidingExpiration);

    void RemoveFromCache(string key);

    bool IsInCache(string key);
}
