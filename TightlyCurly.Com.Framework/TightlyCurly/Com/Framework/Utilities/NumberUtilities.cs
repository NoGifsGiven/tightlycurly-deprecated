namespace TightlyCurly.Com.Framework.Utilities;

public static class NumberUtilities
{
    public static bool IsNumeric(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return IsInt32(value) || IsInt64(value) || IsDecimal(value) || IsFloat(value) || IsLong(value);
        }
        return false;
    }

    private static bool IsInt32(string value)
    {
        int result;
        return int.TryParse(value, out result);
    }

    public static bool IsInt64(string value)
    {
        long result;
        return long.TryParse(value, out result);
    }

    public static bool IsDecimal(string value)
    {
        decimal result;
        return decimal.TryParse(value, out result);
    }

    public static bool IsFloat(string value)
    {
        float result;
        return float.TryParse(value, out result);
    }

    public static bool IsLong(string value)
    {
        long result;
        return long.TryParse(value, out result);
    }
}
