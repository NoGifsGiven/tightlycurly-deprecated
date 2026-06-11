using System;

namespace TightlyCurly.Com.Framework.Utilities;

public static class EnumParser
{
    public static T Parse<T>(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException("value");
        }
        return (T)Enum.Parse(typeof(T), value);
    }
}
