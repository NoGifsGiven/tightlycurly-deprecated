using System;

namespace TightlyCurly.Com.Common.Security;

public class FeedCredentialsParser : IParser
{
    public object Parse(object value)
    {
        string text = value.ToString();
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException("connectionString");
        }
        string[] array = text.Split(new char[1] { ';' });
        FeedCredentials feedCredentials = new FeedCredentials();
        feedCredentials.FeedUri = new Uri(array[0]);
        feedCredentials.Username = array[1];
        feedCredentials.Password = array[2];
        return feedCredentials;
    }
}
