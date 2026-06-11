using System;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Web.Helpers;

namespace TightlyCurly.Com.Web.Presenters;

public class MasterPresenter : IPresenter
{
    protected readonly IMasterView _masterView;

    protected readonly IConfigurationHelper _configurationHelper;

    protected readonly ISettingsHelper _settingsHelper;

    protected readonly ISiteMapWrapper _siteMapWrapper;

    public MasterPresenter(IMasterView masterView, IConfigurationHelper configurationHelper, ISettingsHelper settingsHelper, ISiteMapWrapper siteMapWrapper)
    {
        if (masterView == null)
        {
            throw new ArgumentNullException("masterView");
        }
        if (configurationHelper == null)
        {
            throw new ArgumentNullException("configurationHelper");
        }
        if (settingsHelper == null)
        {
            throw new ArgumentNullException("settingsHelper");
        }
        if (siteMapWrapper == null)
        {
            throw new ArgumentNullException("siteMapWrapper");
        }
        _masterView = masterView;
        _configurationHelper = configurationHelper;
        _settingsHelper = settingsHelper;
        _siteMapWrapper = siteMapWrapper;
    }

    public void SetViewProperties()
    {
        _masterView.EnableAnalytics = _configurationHelper.EnableAnalytics;
        if (!string.IsNullOrEmpty(_siteMapWrapper.PageTitle))
        {
            _masterView.PageTitle = _siteMapWrapper.PageTitle;
        }
        _masterView.SiteBulletin = _settingsHelper.SiteBulletin;
        _masterView.SiteBulletinCssClass = _settingsHelper.SiteBulletinCssClass;
    }
}
