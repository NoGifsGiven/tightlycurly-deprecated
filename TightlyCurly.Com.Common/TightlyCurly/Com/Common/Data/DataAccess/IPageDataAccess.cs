using System.Data;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataAccess;

public interface IPageDataAccess
{
    IDataReader SavePage(Page page);

    IDataReader SavePageContent(PageContent pageContent, Page page);

    IDataReader GetPageById(int pageId);

    IDataReader GetPageByName(string name);

    IDataReader GetPageContentByPageId(int pageId);

    IDataReader GetPageContentById(int pageContentId);

    IDataReader GetAllPages();

    void DeletePageById(int pageId);

    void DeletePageContentById(int pageContentId);

    void SetPageContentActive(int pageId, int pageContentId);
}
