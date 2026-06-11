using System;
using TightlyCurly.Com.Framework.Net;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public class SmtpEmailProvider : IEmailProvider
{
    public void SendMail(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message");
        }
        EmailManager.SendEmail(message);
    }
}
