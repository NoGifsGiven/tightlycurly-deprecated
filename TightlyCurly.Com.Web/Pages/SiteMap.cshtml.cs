using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TightlyCurly.Com.Web.Controllers;

namespace TightlyCurly.Com.Web.Pages;

/// <summary>
/// Replaces SiteMapView.aspx: serves the XML sitemap from the SiteMapController
/// (called in-process), which replaced the SiteMapPresenter.
/// </summary>
public class SiteMapModel : PageModel
{
    private readonly SiteMapController _siteMap;

    public SiteMapModel(SiteMapController siteMap)
    {
        _siteMap = siteMap ?? throw new ArgumentNullException(nameof(siteMap));
    }

    public IActionResult OnGet()
    {
        return _siteMap.GetSiteMap();
    }
}
