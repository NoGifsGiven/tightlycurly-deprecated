using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface IPageDataProvider
{
    Page GetPageById(int pageId);

    Page GetPageByName(string name);

    IEnumerable<PageContent> GetPageContentByPageId(int pageId);

    PageContent GetPageContentById(int pageContentId);

    IEnumerable<Page> GetAllPages();

    Page SavePage(Page page);

    PageContent SavePageContent(PageContent pageContent, Page page);

    void DeletePageById(int pageId);

    void DeletePageContentById(int pageContentId);

    void SetPageContentActive(int pageId, int pageContentId);
}
