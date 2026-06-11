namespace TightlyCurly.Com.Web;

/// <summary>
/// Per-request layout chrome. Replaces the Web Forms master page state: the
/// MasterViewPageFilter populates this from the LayoutController before page
/// execution, and _Layout.cshtml renders from it.
/// </summary>
public class LayoutState
{
    public string PageTitle { get; set; }

    public bool EnableAnalytics { get; set; }

    public string SiteBulletin { get; set; }

    public string SiteBulletinCssClass { get; set; }

    public bool DisplayPicker { get; set; } = true;
}
