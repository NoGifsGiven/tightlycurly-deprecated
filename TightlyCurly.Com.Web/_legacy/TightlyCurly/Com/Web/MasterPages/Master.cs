using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Web.Helpers;
using TightlyCurly.Com.Web.Presenters;
using TightlyCurly.Com.Web.UserControls;

namespace TightlyCurly.Com.Web.MasterPages;

public class Master : MasterPage, IMasterView
{
    private readonly MasterPresenter _masterPresenter;

    protected HtmlHead pageHeader;

    protected HtmlForm MasterForm;

    protected ScriptManager ScriptManager;

    protected SiteMapDataSource SiteMapDataSource;

    protected Panel SiteBulletinContainer;

    protected Literal SiteBulletinContainerText;

    protected Literal PageTitleText;

    protected Menu LeftMenu;

    protected ContentPlaceHolder ContentArea;

    protected Footer Footer;

    protected TightlyCurly.Com.Web.UserControls.AmazonWidget AmazonWidget;

    protected GoogleAnalytics GoogleAnalytics;

    public bool DisplayPicker { get; set; }

    public string PageTitle
    {
        get
        {
            return PageTitleText.Text;
        }
        set
        {
            PageTitleText.Text = value;
        }
    }

    public bool EnableAnalytics
    {
        get
        {
            return ((Control)GoogleAnalytics).Visible;
        }
        set
        {
            ((Control)GoogleAnalytics).Visible = value;
        }
    }

    public string SiteBulletin
    {
        get
        {
            return SiteBulletinContainerText.Text;
        }
        set
        {
            ((Control)SiteBulletinContainer).Visible = !string.IsNullOrEmpty(value);
            SiteBulletinContainerText.Text = value;
        }
    }

    public string SiteBulletinCssClass
    {
        get
        {
            return ((WebControl)SiteBulletinContainer).CssClass;
        }
        set
        {
            ((WebControl)SiteBulletinContainer).CssClass = value;
        }
    }

    public Master()
    {
        DisplayPicker = true;
        _masterPresenter = new MasterPresenter(this, Container.Resolve<IConfigurationHelper>(), Container.Resolve<ISettingsHelper>(), Container.Resolve<ISiteMapWrapper>());
    }

    protected override void OnLoad(EventArgs e)
    {
        ((Control)this).OnLoad(e);
        SetPageOptions();
    }

    private void FormatLeftRail()
    {
        LeftMenu.StaticEnableDefaultPopOutImage = false;
    }

    private void SetPageOptions()
    {
        _masterPresenter.SetViewProperties();
        FormatLeftRail();
    }
}
