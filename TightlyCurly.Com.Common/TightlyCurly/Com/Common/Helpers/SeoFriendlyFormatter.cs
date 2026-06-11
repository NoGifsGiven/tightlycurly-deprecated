using System;

namespace TightlyCurly.Com.Common.Helpers;

public class SeoFriendlyFormatter : IFormatter<string, string>
{
    public string Format(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException("value");
        }
        string text = value;
        text = text.Trim();
        text = text.Replace("\"", string.Empty);
        text = text.Replace("'", string.Empty);
        text = text.Replace("(", string.Empty);
        text = text.Replace(")", string.Empty);
        text = text.Replace("  ", "_");
        text = text.Replace(" ", "_");
        text = text.Replace("/", "_");
        text = text.Replace(",", string.Empty);
        text = text.Replace("#", string.Empty);
        text = text.Replace("?", string.Empty);
        text = text.Replace("&", string.Empty);
        text = text.Replace(".", string.Empty);
        text = text.Replace("!", string.Empty);
        text = text.Replace(",", string.Empty);
        text = text.Replace(":", string.Empty);
        return text.ToLower();
    }
}
