using System;
using System.Web;

namespace TightlyCurly.Com.Web.Helpers;

public class SiteMapWrapper : ISiteMapWrapper
{
    public string PageTitle
    {
        get
        {
            if (SiteMap.CurrentNode != null && !string.IsNullOrEmpty(SiteMap.CurrentNode.Description))
            {
                return SiteMap.CurrentNode.Description;
            }
            return string.Empty;
        }
        set
        {
            throw new NotImplementedException();
        }
    }
}
