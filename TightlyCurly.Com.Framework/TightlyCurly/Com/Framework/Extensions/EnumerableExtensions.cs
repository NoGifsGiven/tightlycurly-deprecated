using System;
using System.Collections.Generic;
using System.Linq;

namespace TightlyCurly.Com.Framework.Extensions;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> values)
    {
        if (values == null || !values.Any())
        {
            return true;
        }
        return false;
    }

    public static string ToDelimitedString<T>(this IEnumerable<T> values, string delimiter)
    {
        if (values.IsNullOrEmpty())
        {
            return string.Empty;
        }
        return string.Join(delimiter, values.Select((T v) => v.ToString()).ToArray());
    }

    public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
    {
        if (values.IsNullOrEmpty())
        {
            return;
        }
        foreach (T value in values)
        {
            action(value);
        }
    }
}
