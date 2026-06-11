using System;
using System.Linq;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Web.Helpers;
using TightlyCurly.Com.Web.Services;

namespace TightlyCurly.Com.Web.Presenters;

public class ContentPagePresenter : IPresenter
{
    protected readonly IContentPageView _view;

    protected readonly IPageService _pageService;

    protected readonly IHttpContextHelper _httpContextHelper;

    protected readonly IResourceHelper _resourceHelper;

    protected Page _selectedPage;

    public ContentPagePresenter(IContentPageView view, IPageService pageService, IHttpContextHelper httpContextHelper, IResourceHelper resourceHelper)
    {
        if (view == null)
        {
            throw new ArgumentNullException("view");
        }
        if (pageService == null)
        {
            throw new ArgumentNullException("pageService");
        }
        if (httpContextHelper == null)
        {
            throw new ArgumentNullException("httpContextHelper");
        }
        if (resourceHelper == null)
        {
            throw new ArgumentNullException("resourceHelper");
        }
        _view = view;
        _pageService = pageService;
        _httpContextHelper = httpContextHelper;
        _resourceHelper = resourceHelper;
    }

    public void GetPageByName(string pageName)
    {
        if (string.IsNullOrEmpty(pageName))
        {
            throw new ArgumentNullException("pageName");
        }
        _selectedPage = _pageService.GetPageByName(pageName);
        switch (_selectedPage.ViewStatus)
        {
            case ViewStatus.None:
            case ViewStatus.NotFound:
            case ViewStatus.NotAuthorized:
                SetHttpStatus();
                break;
            case ViewStatus.UnderConstruction:
                SetUnderConstruction();
                break;
            default:
                SetPageContent();
                break;
        }
    }

    private void SetPageContent()
    {
        if (_selectedPage.Content == null || !_selectedPage.Content.Any() || !_selectedPage.Content.Where((PageContent p) => p.IsActive).Any())
        {
            SetUnderConstruction();
            return;
        }
        PageContent pageContent = _selectedPage.Content.Where((PageContent p) => p.IsActive).Single();
        _view.Content = pageContent.Content;
        _view.PageTitle = pageContent.Title;
        _view.Description = pageContent.Description;
        _view.MetaDescription = pageContent.MetaDescription;
        _view.MetaKeywords = pageContent.MetaKeywords;
    }

    private void SetUnderConstruction()
    {
        _view.Content = _resourceHelper.UnderConstructionContent;
        _view.PageTitle = _resourceHelper.UnderConstructionTitle;
        _view.Description = _resourceHelper.UnderConstructionDescription;
    }

    private void SetHttpStatus()
    {
        _httpContextHelper.SetStatusCode(ViewStatusTranslator.Translate(_selectedPage.ViewStatus));
    }
}
