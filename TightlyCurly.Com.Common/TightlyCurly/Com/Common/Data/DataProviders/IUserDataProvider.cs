using System;
using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface IUserDataProvider
{
    void DeleteUser(User user);

    User SaveUser(User user);

    User GetUser(Func<User, bool> filter);

    IEnumerable<User> GetAllUsers();

    IEnumerable<User> GetUsers(Func<User, bool> filter);

    User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved);
}
