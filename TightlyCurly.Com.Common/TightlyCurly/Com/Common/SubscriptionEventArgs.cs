using System;

namespace TightlyCurly.Com.Common;

public class SubscriptionEventArgs : EventArgs
{
    public string Name { get; private set; }

    public string EmailAddress { get; private set; }

    public SubscriptionEventArgs(string name, string emailAddress)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("name");
        }
        if (string.IsNullOrEmpty(emailAddress))
        {
            throw new ArgumentNullException("emailAddress");
        }
        Name = name;
        EmailAddress = emailAddress;
    }
}
