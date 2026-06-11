using System.Collections.Generic;

namespace TightlyCurly.Com.Framework.Utilities;

public static class DictionaryUtilities<DictionaryType, DictionaryKeyType, DictionaryValueType> where DictionaryType : Dictionary<DictionaryKeyType, DictionaryValueType>
{
    public static bool IsNullOrEmpty(DictionaryType value)
    {
        return value == null || value.Keys.Count == 0;
    }
}
