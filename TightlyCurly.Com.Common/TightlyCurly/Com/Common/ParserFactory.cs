using System;
using TightlyCurly.Com.Common.Security;

namespace TightlyCurly.Com.Common;

internal static class ParserFactory
{
    internal static IParser GetParser(Type type)
    {
        IParser result = null;
        if (type.IsAssignableFrom(typeof(FeedCredentials)))
        {
            result = new FeedCredentialsParser();
        }
        return result;
    }
}
