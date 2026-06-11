using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using TightlyCurly.Com.Common.Data.Mappers;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Framework.Extensions;

namespace TightlyCurly.Com.Common.Data.DataAccess;

public abstract class SqlDataAccessBase
{
    private readonly SqlDatabase _database;

    private readonly TightlyCurly.Com.Common.Data.Mappers.IParameterMapper _parameterMapper;

    private SqlDatabase Database => _database;

    protected SqlDataAccessBase(string connectionString, TightlyCurly.Com.Common.Data.Mappers.IParameterMapper parameterMapper)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException("connectionString");
        }
        if (parameterMapper == null)
        {
            throw new ArgumentNullException("parameterMapper");
        }
        _database = new SqlDatabase(connectionString);
        _parameterMapper = parameterMapper;
    }

    protected SqlDataAccessBase(IConfigurationHelper configurationHelper, TightlyCurly.Com.Common.Data.Mappers.IParameterMapper parameterMapper)
        : this(configurationHelper.DefaultConnectionString, parameterMapper)
    {
        if (configurationHelper == null)
        {
            throw new ArgumentNullException("configurationHelper");
        }
    }

    protected void ExecuteNonQuery(string storedProcedure)
    {
        ExecuteNonQuery(Database, storedProcedure, null);
    }

    protected void ExecuteNonQuery(string storedProcedure, IEnumerable<NamedParameter> namedParameters)
    {
        ExecuteNonQuery(Database, storedProcedure, _parameterMapper.GetParameters(namedParameters));
    }

    protected void ExecuteNonQuery(Database database, string storedProcedure, IEnumerable<DbParameter> parameters)
    {
        using DbConnection dbConnection = database.CreateConnection();
        using DbCommand command = database.GetStoredProcCommand(storedProcedure);
        SetParameters(database, command, parameters);
        dbConnection.Open();
        database.ExecuteNonQuery(command);
    }

    protected IDataReader ExecuteDataReader(string storedProcedure)
    {
        return ExecuteDataReader(Database, storedProcedure, null);
    }

    protected IDataReader ExecuteDataReader(string storedProcedure, IEnumerable<NamedParameter> namedParameters)
    {
        return ExecuteDataReader(Database, storedProcedure, _parameterMapper.GetParameters(namedParameters));
    }

    private IDataReader ExecuteDataReader(Database database, string storedProcedure, IEnumerable<DbParameter> parameters)
    {
        using DbConnection dbConnection = database.CreateConnection();
        using DbCommand command = database.GetStoredProcCommand(storedProcedure);
        SetParameters(database, command, parameters);
        dbConnection.Open();
        return database.ExecuteReader(command);
    }

    private void SetParameters(Database database, DbCommand command, IEnumerable<DbParameter> parameters)
    {
        if (parameters.IsNullOrEmpty())
        {
            return;
        }
        foreach (DbParameter parameter in parameters)
        {
            switch (parameter.Direction)
            {
                case ParameterDirection.Input:
                    database.AddInParameter(command, parameter.ParameterName, parameter.DbType, parameter.Value);
                    break;
                case ParameterDirection.Output:
                    database.AddOutParameter(command, parameter.ParameterName, parameter.DbType, 0);
                    break;
            }
        }
    }
}
