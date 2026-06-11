using System;

namespace TightlyCurly.Com.Common.Security;

public class FeedCredentials
{
    public Uri FeedUri { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public FeedCredentials()
    {
    }

    public FeedCredentials(Uri feedUri, string username, string password)
    {
    }
}
