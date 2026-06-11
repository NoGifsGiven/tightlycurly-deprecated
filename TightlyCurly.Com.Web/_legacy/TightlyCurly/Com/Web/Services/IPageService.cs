using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Services;

public interface IPageService
{
    Page GetPageById(int pageId);

    Page GetPageByName(string name);
}
