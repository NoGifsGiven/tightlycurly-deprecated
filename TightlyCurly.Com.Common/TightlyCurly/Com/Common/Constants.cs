using System;
using System.Configuration;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Net;

namespace TightlyCurly.Com.Common;

public static class Constants
{
    public static class LocaleConstants
    {
        public const int DefaultLcid = 1033;

        public const string DefaultLocale = "en-US";
    }

    public static class MessageConstants
    {
        // TryParse keeps the static initializer from throwing when the settings are
        // absent (e.g. when configuration comes from appsettings.json instead of
        // app.config on ASP.NET Core).
        public static readonly MessageCredentials DefaultCredentials = new MessageCredentials
        {
            Server = ConfigurationManager.AppSettings["SmtpServer"],
            Port = int.TryParse(ConfigurationManager.AppSettings["SmtpServerPort"], out var smtpPort) ? smtpPort : 0,
            RequiresSsl = bool.TryParse(ConfigurationManager.AppSettings["SmtpRequiresSsl"], out var requiresSsl) && requiresSsl,
            Username = ConfigurationManager.AppSettings["SmtpUserName"],
            Password = ConfigurationManager.AppSettings["SmtpPassword"]
        };

        public static string ThankYouMessage = $"Thank you so much for your message! Due to the volume of email I get, it can sometimes take me a few days to a few weeks to reply, but I do my best to answer every email I get.{Environment.NewLine}{Environment.NewLine}Until then, {Environment.NewLine}{Environment.NewLine}Teri";
    }

    public static class SettingsConstants
    {
        public static string ContactPageEnabled = "ContactPageEnabled";

        public static string ContactPageThreshold = "ContactPageThreshold";

        public static string ContactPageDisabledMessage = "ContactPageDisabledMessage";

        public static string CommentsMailTo = "CommentsMailTo";

        public static string SiteBulletin = "SiteBulletin";

        public static string SiteBulletinCssClass = "SiteBulletinCssClass";

        public static string MailFrom = "MailFrom";

        public static string SmtpServer = "SmtpServer";

        public static string SmtpUsername = "SmtpUsername";

        public static string SmtpPassword = "SmtpPassword";

        public static string SmtpUseSsl = "SmtpUserSsl";

        public static string SmtpServerPort = "SmtpServerPort";
    }

    public static class HttpConstants
    {
        public const int ServerError = 500;

        public const int PageNotFound = 404;

        public const int NotAuthorized = 403;

        public const int Ok = 200;
    }

    public const char CredentialsDelimiter = ';';

    public static Locale DefaultLocale = new Locale
    {
        LocaleId = 1,
        LCID = 1033,
        LocaleName = "en-US"
    };
}
