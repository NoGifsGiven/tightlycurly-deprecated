using Microsoft.AspNetCore.Mvc;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Web.Helpers;

namespace TightlyCurly.Com.Web.Controllers;

/// <summary>
/// Replaces MasterPresenter: exposes the layout chrome (formerly the master page's
/// IMasterView properties). MasterViewPageFilter calls this in-process for every
/// Razor Page request to populate LayoutState.
/// </summary>
[ApiController]
[Route("api/layout")]
public class LayoutController : ControllerBase
{
    private readonly IConfigurationHelper _configurationHelper;

    private readonly ISettingsHelper _settingsHelper;

    private readonly ISiteMapWrapper _siteMapWrapper;

    public LayoutController(IConfigurationHelper configurationHelper, ISettingsHelper settingsHelper, ISiteMapWrapper siteMapWrapper)
    {
        _configurationHelper = configurationHelper ?? throw new ArgumentNullException(nameof(configurationHelper));
        _settingsHelper = settingsHelper ?? throw new ArgumentNullException(nameof(settingsHelper));
        _siteMapWrapper = siteMapWrapper ?? throw new ArgumentNullException(nameof(siteMapWrapper));
    }

    // Replaces SetViewProperties.
    [HttpGet]
    public Layout GetLayout()
    {
        return new Layout
        {
            EnableAnalytics = _configurationHelper.EnableAnalytics,
            PageTitle = _siteMapWrapper.PageTitle,
            SiteBulletin = _settingsHelper.SiteBulletin,
            SiteBulletinCssClass = _settingsHelper.SiteBulletinCssClass
        };
    }
}

/// <summary>The site-wide layout chrome (formerly the IMasterView properties).</summary>
public class Layout
{
    public string PageTitle { get; set; }

    public bool EnableAnalytics { get; set; }

    public string SiteBulletin { get; set; }

    public string SiteBulletinCssClass { get; set; }
}
