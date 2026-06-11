namespace TightlyCurly.Com.Common.Helpers;

public interface ISettingsHelper
{
    bool ContactPageEnabled { get; }

    int ContactPageThreshold { get; }

    string ContactPageDisabledMessage { get; }

    string SiteBulletin { get; }

    string SiteBulletinCssClass { get; }

    string MailFrom { get; }

    string CommentsMailTo { get; }

    string SmtpServer { get; }

    string SmtpUsername { get; }

    string SmtpPassword { get; }

    bool SmtpUseSsl { get; }

    int SmtpServerPort { get; }
}
