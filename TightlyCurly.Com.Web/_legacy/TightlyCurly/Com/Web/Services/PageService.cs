using System;
using System.Web.Caching;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Services;

public class PageService : IPageService
{
    protected readonly IPageDataProvider _pageDataProvider;

    protected readonly ICacheHelper _cacheHelper;

    protected readonly IConfigurationHelper _configurationHelper;

    public PageService(IPageDataProvider pageDataProvider, ICacheHelper cacheHelper, IConfigurationHelper configurationHelper)
    {
        if (pageDataProvider == null)
        {
            throw new ArgumentNullException("pageDataProvider");
        }
        if (cacheHelper == null)
        {
            throw new ArgumentNullException("cacheHelper");
        }
        if (configurationHelper == null)
        {
            throw new ArgumentNullException("configurationHelper");
        }
        _pageDataProvider = pageDataProvider;
        _cacheHelper = cacheHelper;
        _configurationHelper = configurationHelper;
    }

    public Page GetPageById(int pageId)
    {
        if (pageId <= 0)
        {
            throw new ArgumentException("pageId cannot be less than or equal to zero.", "pageId");
        }
        Page page;
        if (!_cacheHelper.CacheEnabled || !_cacheHelper.IsInCache("page" + pageId))
        {
            page = _pageDataProvider.GetPageById(pageId);
            if (_cacheHelper.CacheEnabled)
            {
                _cacheHelper.InsertCache("page" + pageId, page, null, _configurationHelper.DefaultCacheExpiration, Cache.NoSlidingExpiration);
            }
        }
        else
        {
            page = (Page)_cacheHelper.RetrieveFromCache("page" + pageId);
        }
        if (page == null)
        {
            throw new InvalidOperationException("Page cannot be found in cache.");
        }
        return page;
    }

    public Page GetPageByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("name");
        }
        Page page;
        if (!_cacheHelper.CacheEnabled || !_cacheHelper.IsInCache("page" + name))
        {
            page = _pageDataProvider.GetPageByName(name);
            if (_cacheHelper.CacheEnabled)
            {
                _cacheHelper.InsertCache("page" + name, page, null, _configurationHelper.DefaultCacheExpiration, Cache.NoSlidingExpiration);
            }
        }
        else
        {
            page = (Page)_cacheHelper.RetrieveFromCache("page" + name);
        }
        if (page == null)
        {
            throw new InvalidOperationException("Page cannot be found in cache.");
        }
        return page;
    }
}
