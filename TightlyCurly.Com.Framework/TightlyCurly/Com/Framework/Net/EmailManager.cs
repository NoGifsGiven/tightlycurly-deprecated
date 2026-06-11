using System.Net;
using System.Net.Mail;

namespace TightlyCurly.Com.Framework.Net;

public static class EmailManager
{
    public static bool SendEmail(Message message)
    {
        MailMessage mailMessage = new MailMessage(message.From, message.To, message.Subject, message.Body);
        if (!string.IsNullOrEmpty(message.Cc))
        {
            mailMessage.CC.Add(message.Cc);
        }
        if (!string.IsNullOrEmpty(message.Bcc))
        {
            mailMessage.Bcc.Add(message.Bcc);
        }
        if (!string.IsNullOrEmpty(message.ReplyTo))
        {
            mailMessage.ReplyToList.Add(new MailAddress(message.ReplyTo));
        }
        mailMessage.IsBodyHtml = message.MessageFormat == MessageFormat.Html;
        SmtpClient smtpClient = new SmtpClient(message.Credentials.Server);
        NetworkCredential credentials = new NetworkCredential(message.Credentials.Username, message.Credentials.Password);
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = message.Credentials.RequiresSsl;
        smtpClient.Credentials = credentials;
        if (message.Credentials.Port > 0)
        {
            smtpClient.Port = message.Credentials.Port;
        }
        smtpClient.Send(mailMessage);
        return true;
    }
}
