using System.Collections;

namespace TightlyCurly.Com.Framework.Utilities;

public static class HashtableUtility
{
    public static bool IsNullOrEmpty(Hashtable hashTable)
    {
        return hashTable == null || hashTable.Keys.Count == 0;
    }
}
