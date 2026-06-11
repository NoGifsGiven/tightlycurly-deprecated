namespace TightlyCurly.Com.Web.Presenters;

public interface IMasterView
{
    string PageTitle { get; set; }

    bool EnableAnalytics { get; set; }

    string SiteBulletin { get; set; }

    string SiteBulletinCssClass { get; set; }
}
