using System.Text.RegularExpressions;
using TightlyCurly.Com.Framework.Common;

namespace TightlyCurly.Com.Framework.Utilities;

public static class StringUtility
{
    public static bool IsEmailAddress(this string value)
    {
        return !string.IsNullOrEmpty(value) && Regex.IsMatch(value, Constants.EmailAddressPattern, RegexOptions.Compiled);
    }

    public static string Remove(string value, string stringToRemove)
    {
        if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(stringToRemove) && value.IndexOf(stringToRemove) >= 0)
        {
            value = value.Replace(stringToRemove, string.Empty);
        }
        return value;
    }

    public static bool IsUppercase(string str)
    {
        string pattern = "[a-z]";
        try
        {
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(str))
            {
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
