using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Framework.Web.Utilities;
using TightlyCurly.Com.Web.Controllers;
using TightlyCurly.Com.Web.Helpers;

namespace TightlyCurly.Com.Web.Pages;

/// <summary>
/// Replaces ContentPage.aspx. The page content comes from the ContentPagesController
/// (called in-process), which replaced the ContentPagePresenter; error statuses come
/// back on the ActionResult instead of through IHttpContextHelper.
/// </summary>
public class ContentModel : PageModel
{
    private readonly ContentPagesController _contentPages;

    private readonly IConfigurationHelper _configurationHelper;

    public ContentModel(ContentPagesController contentPages, IConfigurationHelper configurationHelper)
    {
        _contentPages = contentPages ?? throw new ArgumentNullException(nameof(contentPages));
        _configurationHelper = configurationHelper ?? throw new ArgumentNullException(nameof(configurationHelper));
    }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }

    public string PageTitle { get; set; }

    public string Description { get; set; }

    public new string Content { get; set; }

    public IActionResult OnGet(string pageName)
    {
        string name = pageName;
        if (string.IsNullOrEmpty(name))
        {
            name = TextEncoder.SafeEncode(Request.Query["pageName"]);
        }
        if (string.IsNullOrEmpty(name))
        {
            name = _configurationHelper.DefaultPage;
        }

        var result = _contentPages.GetPageByName(name);
        ContentPage page = result.GetValue();
        if (page == null)
        {
            return new StatusCodeResult(result.GetStatusCode());
        }

        Content = page.Content;
        PageTitle = page.PageTitle;
        Description = page.Description;
        MetaDescription = page.MetaDescription;
        MetaKeywords = page.MetaKeywords;

        ViewData["Title"] = PageTitle;
        ViewData["MetaDescription"] = MetaDescription;
        ViewData["MetaKeywords"] = MetaKeywords;
        return Page();
    }
}
