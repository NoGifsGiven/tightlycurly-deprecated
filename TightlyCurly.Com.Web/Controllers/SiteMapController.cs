using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Controllers;

/// <summary>
/// Replaces SiteMapPresenter. The ContentResult carries the text/xml content type,
/// replacing the IHttpContextHelper.SetMimeType side effect.
/// </summary>
[ApiController]
[Route("api/sitemap")]
public class SiteMapController : ControllerBase
{
    private readonly ISiteMapDataProvider _siteMapDataProvider;

    public SiteMapController(ISiteMapDataProvider siteMapDataProvider)
    {
        _siteMapDataProvider = siteMapDataProvider ?? throw new ArgumentNullException(nameof(siteMapDataProvider));
    }

    [HttpGet]
    public ContentResult GetSiteMap()
    {
        IEnumerable<SiteMapEntry> allSiteMapEntries = _siteMapDataProvider.GetAllSiteMapEntries();
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        XElement urlset = new XElement(ns + "urlset",
            new XAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9"),
            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
            new XAttribute(xsi + "schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
            allSiteMapEntries.Select(u => new XElement(ns + "url",
                new XElement(ns + "loc", u.Uri.ToString().Replace("http//", "http://")))));
        return Content(urlset.ToString(), "text/xml");
    }
}
