using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using TightlyCurly.Com.Framework.Data.Base;

namespace TightlyCurly.Com.Framework.Data.DataAccess;

public class SqlDataAccess : DataAccessBase
{
    protected SqlDatabase _database = null;

    protected string _databaseName = "";

    public SqlDatabase Database
    {
        get
        {
            return _database;
        }
        set
        {
            _database = value;
        }
    }

    public SqlDataAccess()
    {
    }

    public SqlDataAccess(string connectionString)
    {
        _database = new SqlDatabase(connectionString);
    }
}
