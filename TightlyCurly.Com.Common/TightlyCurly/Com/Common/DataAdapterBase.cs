using System;
using TightlyCurly.Com.Common.Data.DataAccess;

namespace TightlyCurly.Com.Common;

public abstract class DataAdapterBase
{
    protected string _connectionString;

    private ObjectDataAccess _dataAccess;

    protected string ConnectionString => _connectionString ?? throw new InvalidOperationException("ConnectionString not set");

    protected ObjectDataAccess DataAccess
    {
        get
        {
            if (_dataAccess == null)
            {
                _dataAccess = new ObjectDataAccess(ConnectionString);
            }
            return _dataAccess;
        }
    }

    protected void SetConnectionString(string connectionString)
    {
        if (_connectionString != null && _connectionString != connectionString)
        {
            _dataAccess?.Dispose();
            _dataAccess = null;
        }
        _connectionString = connectionString;
    }

    protected void SetConnectionString(Func<string> connectionStringFactory)
    {
        SetConnectionString(connectionStringFactory());
    }
}
