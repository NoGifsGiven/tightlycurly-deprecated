using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TightlyCurly.Com.Common.Helpers;

namespace TightlyCurly.Com.Web.Pages;

/// <summary>Replaces _Default.aspx: redirect to the default content page.</summary>
public class IndexModel : PageModel
{
    private readonly IConfigurationHelper _configurationHelper;

    public IndexModel(IConfigurationHelper configurationHelper)
    {
        _configurationHelper = configurationHelper;
    }

    public IActionResult OnGet()
    {
        string defaultPage = "welcome";
        try
        {
            if (!string.IsNullOrEmpty(_configurationHelper.DefaultPage))
            {
                defaultPage = _configurationHelper.DefaultPage;
            }
        }
        catch
        {
            // Fall back to the conventional default page name.
        }
        return RedirectToPage("/Content", new { pageName = defaultPage });
    }
}
