using System.Web;
using TightlyCurly.Com.Framework.Web.Caching;

namespace TightlyCurly.Com.Web;

public class PageContext
{
    public string UserAgent { get; set; }

    public static PageContext Current
    {
        get
        {
            HttpContext current = HttpContext.Current;
            if (!CacheHelper.IsInCache(current.Session.SessionID + "PageContext"))
            {
                CacheHelper.InsertCache(current.Session.SessionID + "PageContext", NewObject());
            }
            return (PageContext)CacheHelper.RetrieveFromCache(current.Session.SessionID + "PageContext");
        }
    }

    public static PageContext NewObject()
    {
        PageContext pageContext = new PageContext();
        pageContext.UserAgent = HttpContext.Current.Request.UserAgent;
        return pageContext;
    }
}
