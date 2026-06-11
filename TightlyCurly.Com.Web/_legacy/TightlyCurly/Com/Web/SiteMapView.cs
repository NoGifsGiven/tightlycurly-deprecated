using System;
using System.Web.UI;
using TightlyCurly.Com.Web.Presenters;

namespace TightlyCurly.Com.Web;

public class SiteMapView : PageBase<SiteMapPresenter, ISiteMapView>, ISiteMapView
{
    public string SiteMapEntries
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            ((Page)this).Response.Clear();
            ((Page)this).Response.Write(value);
        }
    }

    public SiteMapView()
    {
        Initialize(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Presenter.GetSiteMapEntries();
    }
}
