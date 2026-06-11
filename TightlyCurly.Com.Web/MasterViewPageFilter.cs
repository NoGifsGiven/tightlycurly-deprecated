using Microsoft.AspNetCore.Mvc.Filters;
using TightlyCurly.Com.Web.Controllers;

namespace TightlyCurly.Com.Web;

/// <summary>
/// Runs the LayoutController for every Razor Page request, populating LayoutState
/// the same way the Web Forms master page (via MasterPresenter) did in OnLoad.
/// </summary>
public class MasterViewPageFilter : IPageFilter
{
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        var layoutState = context.HttpContext.RequestServices.GetService<LayoutState>();
        if (layoutState == null)
        {
            return;
        }
        try
        {
            Layout layout = context.HttpContext.RequestServices
                .GetRequiredService<LayoutController>()
                .GetLayout();
            layoutState.EnableAnalytics = layout.EnableAnalytics;
            if (!string.IsNullOrEmpty(layout.PageTitle))
            {
                layoutState.PageTitle = layout.PageTitle;
            }
            layoutState.SiteBulletin = layout.SiteBulletin;
            layoutState.SiteBulletinCssClass = layout.SiteBulletinCssClass;
        }
        catch
        {
            // Layout chrome (bulletin, analytics flag) is best-effort; the page should
            // still render if the settings database is unavailable.
        }
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }
}
