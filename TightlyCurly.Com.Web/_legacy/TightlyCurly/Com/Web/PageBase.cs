using System;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Web.UserControls;

namespace TightlyCurly.Com.Web;

public abstract class PageBase<T, U> : Page where T : class
{
    protected IConfigurationHelper _configuration = Container.Resolve<IConfigurationHelper>();

    private object _view;

    private T _presenter;

    protected T Presenter => _presenter ?? (_presenter = PresenterFactory.GetPresenter<T, U>(_view));

    public MessageBox MessageBox => (MessageBox)(object)((Control)((Page)this).Master).FindControl("MessageBoxControl");

    protected void Initialize(object view)
    {
        _view = view;
    }

    protected override void OnPreInit(EventArgs e)
    {
        SetBrowserCapabilities();
        ((Page)this).OnPreInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        if (SiteMap.CurrentNode != null && !string.IsNullOrEmpty(SiteMap.CurrentNode.Title))
        {
            ((Page)this).Title = SiteMap.CurrentNode.Title;
        }
        ((Control)this).OnLoad(e);
    }

    protected void SetPageMetadata(string title)
    {
        Control obj = ((Control)((Page)this).Master).FindControl("PageTitleText");
        Literal val = (Literal)(object)((obj is Literal) ? obj : null);
        if (val != null && !string.IsNullOrEmpty(title))
        {
            val.Text = title;
            ((Page)this).Title = title;
        }
    }

    private void SetBrowserCapabilities()
    {
        if (PageContext.Current.UserAgent.IndexOf("WebKit") >= 0 || PageContext.Current.UserAgent.IndexOf("Chrome") >= 0)
        {
            ((HttpCapabilitiesBase)((Page)this).Request.Browser).Adapters.Clear();
            ((Control)this).Page.ClientTarget = "uplevel";
        }
    }
}
