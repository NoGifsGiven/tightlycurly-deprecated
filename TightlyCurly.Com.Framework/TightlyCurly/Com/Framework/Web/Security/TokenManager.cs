using System;
using TightlyCurly.Com.Framework.Web.Caching;

namespace TightlyCurly.Com.Framework.Web.Security;

public static class TokenManager
{
    public static void InsertToken(string key, string token)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("key");
        }
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentNullException("token");
        }
        CacheHelper.InsertCache(key, token);
    }

    public static string GetToken(string key)
    {
        return CacheHelper.RetrieveFromCache(key) as string;
    }

    public static void RemoveToken(string key)
    {
        CacheHelper.RemoveFromCache(key);
    }
}
