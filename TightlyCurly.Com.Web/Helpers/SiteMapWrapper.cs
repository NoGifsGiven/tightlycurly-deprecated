namespace TightlyCurly.Com.Web.Helpers;

/// <summary>
/// The Web Forms site map provider (Web.sitemap) has no ASP.NET Core equivalent;
/// page titles are now set by the pages themselves, so this returns no override.
/// </summary>
public class SiteMapWrapper : ISiteMapWrapper
{
    public string PageTitle
    {
        get => string.Empty;
        set => throw new NotImplementedException();
    }
}
