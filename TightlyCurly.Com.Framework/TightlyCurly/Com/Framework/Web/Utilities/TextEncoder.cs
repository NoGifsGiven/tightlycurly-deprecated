using System.Text.RegularExpressions;
using System.Web;

namespace TightlyCurly.Com.Framework.Web.Utilities;

public static class TextEncoder
{
    private const string ScriptObjectPattern = "<script.*?>.*?</script>";

    public static string SafeEncode(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            value = Regex.Replace(value, ScriptObjectPattern, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            // HttpContext.Current.Server.HtmlEncode is gone; System.Web.HttpUtility ships with .NET.
            value = HttpUtility.HtmlEncode(value);
        }
        return value;
    }
}
