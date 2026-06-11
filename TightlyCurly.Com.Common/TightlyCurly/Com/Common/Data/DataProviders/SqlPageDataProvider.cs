using System;
using System.Collections.Generic;
using System.Linq;
using TightlyCurly.Com.Common.Data.Builders;
using TightlyCurly.Com.Common.Data.DataAccess;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public class SqlPageDataProvider : DataAdapterBase, IPageDataProvider
{
    protected readonly IPageBuilder _pageBuilder;

    protected readonly IPageDataAccess _dataAccess;

    public SqlPageDataProvider(IPageBuilder pageBuilder, IPageDataAccess pageDataAccess)
    {
        if (pageBuilder == null)
        {
            throw new ArgumentNullException("pageBuilder");
        }
        if (pageDataAccess == null)
        {
            throw new ArgumentNullException("pageDataAccess");
        }
        _pageBuilder = pageBuilder;
        _dataAccess = pageDataAccess;
    }

    public Page GetPageById(int pageId)
    {
        if (pageId <= 0)
        {
            throw new ArgumentException("pageId cannot be less than or equal to zero.", "pageId");
        }
        Page page = _pageBuilder.BuildPages(() => _dataAccess.GetPageById(pageId)).FirstOrDefault();
        if (page != null)
        {
            page.Content = GetPageContentByPageId(page.PageId);
        }
        return page;
    }

    public Page GetPageByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("name");
        }
        Page page = _pageBuilder.BuildPages(() => _dataAccess.GetPageByName(name)).FirstOrDefault();
        if (page != null)
        {
            page.Content = GetPageContentByPageId(page.PageId);
        }
        return page;
    }

    public IEnumerable<PageContent> GetPageContentByPageId(int pageId)
    {
        if (pageId <= 0)
        {
            throw new ArgumentException("pageId cannot be less than or equal to zero.", "pageId");
        }
        return _pageBuilder.BuildPageContents(() => _dataAccess.GetPageContentByPageId(pageId));
    }

    public PageContent GetPageContentById(int pageContentId)
    {
        if (pageContentId <= 0)
        {
            throw new ArgumentException("pageContentId cannot be less than or equal to zero.", "pageContentId");
        }
        return _pageBuilder.BuildPageContents(() => _dataAccess.GetPageContentById(pageContentId)).FirstOrDefault();
    }

    public IEnumerable<Page> GetAllPages()
    {
        IEnumerable<Page> enumerable = _pageBuilder.BuildPages(() => _dataAccess.GetAllPages());
        foreach (Page item in enumerable)
        {
            item.Content = GetPageContentByPageId(item.PageId);
        }
        return enumerable;
    }

    public Page SavePage(Page page)
    {
        if (page == null)
        {
            throw new ArgumentNullException("page");
        }
        page = _pageBuilder.BuildPages(() => _dataAccess.SavePage(page)).FirstOrDefault();
        if (page != null)
        {
            page.Content = GetPageContentByPageId(page.PageId);
        }
        return page;
    }

    public PageContent SavePageContent(PageContent pageContent, Page page)
    {
        if (pageContent == null)
        {
            throw new ArgumentNullException("pageContent");
        }
        if (page == null)
        {
            throw new ArgumentNullException("page");
        }
        return _pageBuilder.BuildPageContents(() => _dataAccess.SavePageContent(pageContent, page)).FirstOrDefault();
    }

    public void DeletePageById(int pageId)
    {
        if (pageId <= 0)
        {
            throw new ArgumentException("pageId cannot be less than or equal to zero.", "pageId");
        }
        _dataAccess.DeletePageById(pageId);
    }

    public void DeletePageContentById(int pageContentId)
    {
        if (pageContentId <= 0)
        {
            throw new ArgumentException("pageContentId cannot be less than or equal to zero.", "pageContentId");
        }
        _dataAccess.DeletePageContentById(pageContentId);
    }

    public void SetPageContentActive(int pageId, int pageContentId)
    {
        if (pageId <= 0)
        {
            throw new ArgumentException("pageId cannot be less than or equal to zero.", "pageId");
        }
        if (pageContentId <= 0)
        {
            throw new ArgumentException("pageContentId cannot be less than or equal to zero.", "pageContentId");
        }
        _dataAccess.SetPageContentActive(pageId, pageContentId);
    }
}
