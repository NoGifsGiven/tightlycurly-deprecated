using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

/// <summary>
/// Replaces LinqUserDataProvider (EF Core, originally LINQ to SQL): the same queries
/// against the ALLIGOSHEE_HAIR_ADMIN database, issued through Dapper. The
/// Func-based filters load the user list and filter in memory, exactly as the
/// EF version did.
/// </summary>
public class DapperUserDataProvider : IUserDataProvider
{
    private const string SelectColumns =
        "UserId, Username, Password, EmailAddress, IsLockedOut, PasswordQuestion, PasswordAnswer, " +
        "Comment, IsApproved, CreationDate, LastLoginDate, LastActivityDate, LastPasswordChangedDate, LastLockoutDate";

    private readonly IConfigurationHelper _configurationHelper;

    public DapperUserDataProvider(IConfigurationHelper configurationHelper)
    {
        _configurationHelper = configurationHelper ?? throw new ArgumentNullException(nameof(configurationHelper));
    }

    private SqlConnection CreateConnection()
    {
        return new SqlConnection(_configurationHelper.AdminConnectionString);
    }

    public void DeleteUser(User user)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM dbo.Users WHERE UserId = @UserId", new { user.UserId });
    }

    public User SaveUser(User user)
    {
        using var connection = CreateConnection();
        if (user.UserId == 0)
        {
            InsertUser(connection, user);
        }
        else
        {
            connection.Execute(
                @"UPDATE dbo.Users
                  SET Username = @Username,
                      Password = @Password,
                      EmailAddress = @EmailAddress,
                      PasswordQuestion = @PasswordQuestion,
                      PasswordAnswer = @PasswordAnswer,
                      IsApproved = @IsApproved
                  WHERE UserId = @UserId",
                new
                {
                    user.Username,
                    user.Password,
                    user.EmailAddress,
                    user.PasswordQuestion,
                    user.PasswordAnswer,
                    user.IsApproved,
                    user.UserId
                });
        }
        return user;
    }

    public User GetUser(Func<User, bool> filter)
    {
        return GetAllUsers().FirstOrDefault(filter);
    }

    public IEnumerable<User> GetAllUsers()
    {
        using var connection = CreateConnection();
        return connection.Query<User>($"SELECT {SelectColumns} FROM dbo.Users").ToList();
    }

    public IEnumerable<User> GetUsers(Func<User, bool> filter)
    {
        return GetAllUsers().Where(filter);
    }

    public User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved)
    {
        var user = new User
        {
            Username = username,
            Password = password,
            EmailAddress = email,
            PasswordQuestion = passwordQuestion,
            PasswordAnswer = passwordAnswer,
            IsApproved = isApproved,
            CreationDate = DateTimeOffset.UtcNow
        };

        using var connection = CreateConnection();
        InsertUser(connection, user);
        return user;
    }

    private static void InsertUser(SqlConnection connection, User user)
    {
        user.UserId = connection.ExecuteScalar<int>(
            @"INSERT INTO dbo.Users
                  (Username, Password, EmailAddress, IsLockedOut, PasswordQuestion, PasswordAnswer,
                   Comment, IsApproved, CreationDate, LastLoginDate, LastActivityDate, LastPasswordChangedDate, LastLockoutDate)
              VALUES
                  (@Username, @Password, @EmailAddress, @IsLockedOut, @PasswordQuestion, @PasswordAnswer,
                   @Comment, @IsApproved, @CreationDate, @LastLoginDate, @LastActivityDate, @LastPasswordChangedDate, @LastLockoutDate);
              SELECT CAST(SCOPE_IDENTITY() AS int);",
            user);
    }
}
