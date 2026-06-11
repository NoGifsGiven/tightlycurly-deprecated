using System;

namespace TightlyCurly.Com.Framework.Extensions;

public static class StringExtensions
{
    public static Uri ToUri(string value, UriKind uriKind)
    {
        Uri result = null;
        Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result);
        return result;
    }

    public static Uri ToUri(this string value)
    {
        return ToUri(value, UriKind.RelativeOrAbsolute);
    }
}
