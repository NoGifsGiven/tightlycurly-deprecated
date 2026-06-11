using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Framework.Extensions;
using TightlyCurly.Com.Framework.Web.Utilities;
using TightlyCurly.Com.Web.MasterPages;
using TightlyCurly.Com.Web.Presenters;

namespace TightlyCurly.Com.Web;

public class ContentPage : PageBase<ContentPagePresenter, IContentPageView>, IContentPageView
{
    protected Literal PageContent;

    public string MetaDescription
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0012: Expected O, but got Unknown
            if (!string.IsNullOrEmpty(value))
            {
                HtmlMeta val = new HtmlMeta();
                val.Name = "description";
                val.Content = value;
                HtmlMeta val2 = val;
                ((Control)((Page)this).Header).Controls.Add((Control)(object)val2);
            }
        }
    }

    public string MetaKeywords
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //IL_0012: Expected O, but got Unknown
            if (!string.IsNullOrEmpty(value))
            {
                HtmlMeta val = new HtmlMeta();
                val.Name = "keywords";
                val.Content = value;
                HtmlMeta val2 = val;
                ((Control)((Page)this).Header).Controls.Add((Control)(object)val2);
            }
        }
    }

    public string PageTitle
    {
        get
        {
            return ((Page)this).Title;
        }
        set
        {
            ((Page)this).Title = value;
        }
    }

    public string Description
    {
        get
        {
            if (((Page)this).Master is Master master)
            {
                Control obj = ((Control)master).FindControl("PageTitleText");
                Literal val = (Literal)(object)((obj is Literal) ? obj : null);
                if (val != null)
                {
                    return val.Text;
                }
            }
            return string.Empty;
        }
        set
        {
            if (((Page)this).Master is Master master)
            {
                Control obj = ((Control)master).FindControl("PageTitleText");
                Literal val = (Literal)(object)((obj is Literal) ? obj : null);
                if (val != null)
                {
                    val.Text = value;
                }
            }
        }
    }

    public string Content
    {
        get
        {
            return PageContent.Text;
        }
        set
        {
            PageContent.Text = value;
        }
    }

    public ContentPage()
    {
        Initialize(this);
    }

    protected override void OnLoad(EventArgs e)
    {
        IHttpContextHelper httpContextHelper = Container.Resolve<IHttpContextHelper>();
        try
        {
            string text = (((IDictionary<string, object>)((Control)this).Page.RouteData.Values).IsNullOrEmpty() ? TextEncoder.SafeEncode(httpContextHelper.QueryString["pageName"]) : ((Control)this).Page.RouteData.Values["pageName"].ToString());
            if (string.IsNullOrEmpty(text))
            {
                IConfigurationHelper configurationHelper = Container.Resolve<IConfigurationHelper>();
                text = configurationHelper.DefaultPage;
            }
            base.Presenter.GetPageByName(text);
        }
        catch (ArgumentNullException ex)
        {
            if (!(ex.ParamName == "pageName"))
            {
                throw;
            }
            httpContextHelper.SetStatusCode(500);
        }
        base.OnLoad(e);
    }
}
