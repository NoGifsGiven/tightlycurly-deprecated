// Minimal compatibility implementation of the Microsoft Enterprise Library Data Access
// Application Block API surface used by this solution, backed by Microsoft.Data.SqlClient.
// Enterprise Library was never ported to modern .NET; this shim keeps the existing
// SqlDataAccessBase/SqlDataAccess code compiling and working on .NET 10.
using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    public abstract class Database
    {
        protected Database(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public abstract DbConnection CreateConnection();

        public abstract DbCommand GetStoredProcCommand(string storedProcedureName);

        public virtual void AddInParameter(DbCommand command, string name, DbType dbType, object value)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = BuildParameterName(name);
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        public virtual void AddOutParameter(DbCommand command, string name, DbType dbType, int size)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = BuildParameterName(name);
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Output;
            parameter.Size = size;
            command.Parameters.Add(parameter);
        }

        public virtual int ExecuteNonQuery(DbCommand command)
        {
            if (command.Connection != null && command.Connection.State == ConnectionState.Open)
            {
                return command.ExecuteNonQuery();
            }
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                command.Connection = connection;
                return command.ExecuteNonQuery();
            }
        }

        public virtual IDataReader ExecuteReader(DbCommand command)
        {
            if (command.Connection != null && command.Connection.State == ConnectionState.Open)
            {
                return command.ExecuteReader();
            }
            // Open a dedicated connection that is released when the reader is closed,
            // matching the behavior of the original Enterprise Library implementation.
            DbConnection connection = CreateConnection();
            connection.Open();
            command.Connection = connection;
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public virtual object GetParameterValue(DbCommand command, string name)
        {
            return command.Parameters[BuildParameterName(name)].Value;
        }

        protected virtual string BuildParameterName(string name)
        {
            return name.StartsWith("@", StringComparison.Ordinal) ? name : $"@{name}";
        }
    }
}

namespace Microsoft.Practices.EnterpriseLibrary.Data.Sql
{
    public class SqlDatabase : Database
    {
        public SqlDatabase(string connectionString)
            : base(connectionString)
        {
        }

        public override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public override DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentNullException("storedProcedureName");
            }
            return new SqlCommand(storedProcedureName)
            {
                CommandType = CommandType.StoredProcedure
            };
        }
    }
}
