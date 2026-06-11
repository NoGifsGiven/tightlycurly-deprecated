using System;
using System.Collections.Generic;
using TightlyCurly.Com.Common.Data.DataAccess;
using TightlyCurly.Com.Common.Data.DataProviders;

namespace TightlyCurly.Com.Common.Model;

public class User : IDataEntity
{
    private static IUserDataProvider DataAdapter => Container.Resolve<IUserDataProvider>();

    public List<UsersRole> UsersRoles { get; set; } = new List<UsersRole>();

    [System.ComponentModel.DataAnnotations.Key]
    public int UserId { get; set; }

    public string Username { get; set; }

    public string EmailAddress { get; set; }

    public bool? IsLockedOut { get; set; }

    public string PasswordQuestion { get; set; }

    public string PasswordAnswer { get; set; }

    public string Comment { get; set; }

    public string Password { get; set; }

    public bool? IsApproved { get; set; }

    public DateTimeOffset? CreationDate { get; set; }

    public DateTimeOffset? LastLoginDate { get; set; }

    public DateTimeOffset? LastActivityDate { get; set; }

    public DateTimeOffset? LastPasswordChangedDate { get; set; }

    public DateTimeOffset? LastLockoutDate { get; set; }

    public static bool Exists(int userId)
    {
        return DataAdapter.GetUser(u => u.UserId == userId) != null;
    }

    public static bool Exists(string username)
    {
        var user = DataAdapter.GetUser(u => u.Username == username);
        return user != null;
    }

    public static bool Exists(string username, string password)
    {
        return DataAdapter.GetUser(u => u.Username == username && u.Password == password) != null;
    }

    public static bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        var user = GetUser(u => u.Username == username && u.Password == oldPassword);
        if (user == null)
        {
            return false;
        }
        user.Password = newPassword;
        SaveUser(user);
        return true;
    }

    public static IEnumerable<User> GetAllUsers()
    {
        return DataAdapter.GetAllUsers();
    }

    public static IEnumerable<User> GetUsers(Func<User, bool> filter)
    {
        return DataAdapter.GetUsers(filter);
    }

    public static User GetUser(Func<User, bool> filter)
    {
        return DataAdapter.GetUser(filter);
    }

    public static User SaveUser(User user)
    {
        if (user.UserId < 0)
        {
            throw new ArgumentException("UserId cannot be less than zero");
        }
        return DataAdapter.SaveUser(user);
    }

    public static void DeleteUser(User user)
    {
        DataAdapter.DeleteUser(user);
    }

    public static User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved)
    {
        return DataAdapter.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved);
    }

    public void Detach()
    {
        UsersRoles = new List<UsersRole>();
    }
}
