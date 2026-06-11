using System.Data;

namespace TightlyCurly.Com.Framework.Extensions;

public static class DataReaderExtensions
{
    public static bool ColumnExists(this IDataReader reader, string columnName)
    {
        if (string.IsNullOrEmpty(columnName))
        {
            return false;
        }
        reader.GetSchemaTable().DefaultView.RowFilter = $"ColumnName= '{columnName}'";
        return reader.GetSchemaTable().DefaultView.Count > 0;
    }
}
