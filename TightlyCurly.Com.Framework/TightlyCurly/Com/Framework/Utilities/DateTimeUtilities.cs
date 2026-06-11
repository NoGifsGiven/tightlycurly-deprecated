using System;

namespace TightlyCurly.Com.Framework.Utilities;

public static class DateTimeUtilities
{
    public static object ReturnNullIfEmpty(DateTimeOffset value)
    {
        if (value.Equals(DateTimeOffset.MinValue))
        {
            return null;
        }
        return value;
    }

    public static DateTimeOffset ReturnDateTimeFromNull(object value)
    {
        if (value == null)
        {
            return DateTimeOffset.MinValue;
        }
        return (DateTimeOffset)value;
    }
}
