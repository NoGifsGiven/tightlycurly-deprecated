using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

/// <summary>
/// Replaces LinqRoleDataProvider (EF Core, originally LINQ to SQL): the same queries
/// against the ALLIGOSHEE_HAIR_ADMIN database, issued through Dapper.
/// </summary>
public class DapperRoleDataProvider : IRoleDataProvider
{
    private const string SelectColumns = "RoleId, RoleName, Description, CreatedDate, UpdatedDate";

    private readonly IConfigurationHelper _configurationHelper;

    public DapperRoleDataProvider(IConfigurationHelper configurationHelper)
    {
        _configurationHelper = configurationHelper ?? throw new ArgumentNullException(nameof(configurationHelper));
    }

    private SqlConnection CreateConnection()
    {
        return new SqlConnection(_configurationHelper.AdminConnectionString);
    }

    public void DeleteRole(Role role)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM dbo.Roles WHERE RoleId = @RoleId", new { role.RoleId });
    }

    public Role SaveRole(Role role)
    {
        using var connection = CreateConnection();
        if (role.RoleId == 0)
        {
            InsertRole(connection, role);
        }
        else
        {
            connection.Execute(
                @"UPDATE dbo.Roles
                  SET RoleName = @RoleName,
                      Description = @Description,
                      CreatedDate = @CreatedDate,
                      UpdatedDate = @UpdatedDate
                  WHERE RoleId = @RoleId",
                new
                {
                    role.RoleName,
                    role.Description,
                    role.CreatedDate,
                    role.UpdatedDate,
                    role.RoleId
                });
        }
        return role;
    }

    public Role GetRole(Func<Role, bool> filter)
    {
        return GetAllRoles().FirstOrDefault(filter);
    }

    public IEnumerable<Role> GetAllRoles()
    {
        using var connection = CreateConnection();
        return connection.Query<Role>($"SELECT {SelectColumns} FROM dbo.Roles").ToList();
    }

    public IEnumerable<Role> GetRoles(Func<Role, bool> filter)
    {
        return GetAllRoles().Where(filter);
    }

    public Role CreateRole(string name, string description, DateTimeOffset createdDate, DateTimeOffset updatedDate)
    {
        var role = new Role
        {
            RoleName = name,
            Description = description,
            CreatedDate = createdDate,
            UpdatedDate = updatedDate
        };

        using var connection = CreateConnection();
        InsertRole(connection, role);
        return role;
    }

    private static void InsertRole(SqlConnection connection, Role role)
    {
        role.RoleId = connection.ExecuteScalar<int>(
            @"INSERT INTO dbo.Roles (RoleName, Description, CreatedDate, UpdatedDate)
              VALUES (@RoleName, @Description, @CreatedDate, @UpdatedDate);
              SELECT CAST(SCOPE_IDENTITY() AS int);",
            role);
    }
}
