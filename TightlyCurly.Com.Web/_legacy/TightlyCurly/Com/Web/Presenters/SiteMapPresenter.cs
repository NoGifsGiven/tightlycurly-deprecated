using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Presenters;

public class SiteMapPresenter : IPresenter
{
    protected readonly ISiteMapView _siteMapView;

    protected readonly ISiteMapDataProvider _siteMapDataProvider;

    protected readonly IHttpContextHelper _httpContextHelper;

    public SiteMapPresenter(ISiteMapView siteMapView, ISiteMapDataProvider siteMapDataProvider, IHttpContextHelper httpContextHelper)
    {
        if (siteMapView == null)
        {
            throw new ArgumentNullException("siteMapView");
        }
        if (siteMapDataProvider == null)
        {
            throw new ArgumentNullException("siteMapDataProvider");
        }
        if (httpContextHelper == null)
        {
            throw new ArgumentNullException("httpContextHelper");
        }
        _siteMapView = siteMapView;
        _siteMapDataProvider = siteMapDataProvider;
        _httpContextHelper = httpContextHelper;
    }

    public void GetSiteMapEntries()
    {
        //IL_0069: Unknown result type (might be due to invalid IL or missing references)
        //IL_006f: Expected O, but got Unknown
        //IL_0086: Unknown result type (might be due to invalid IL or missing references)
        //IL_008c: Expected O, but got Unknown
        //IL_009f: Unknown result type (might be due to invalid IL or missing references)
        //IL_00a5: Expected O, but got Unknown
        //IL_00bd: Unknown result type (might be due to invalid IL or missing references)
        //IL_00c3: Expected O, but got Unknown
        _httpContextHelper.SetMimeType("text/xml");
        IEnumerable<SiteMapEntry> allSiteMapEntries = _siteMapDataProvider.GetAllSiteMapEntries();
        XNamespace ns = XNamespace.op_Implicit("http://www.sitemaps.org/schemas/sitemap/0.9");
        XNamespace val = XNamespace.op_Implicit("http://www.w3.org/2001/XMLSchema-instance");
        XElement val2 = new XElement(ns + "urlset", new object[4]
        {
            (object)new XAttribute(XName.op_Implicit("xmlns"), (object)"http://www.sitemaps.org/schemas/sitemap/0.9"),
            (object)new XAttribute(XNamespace.Xmlns + "xsi", (object)"http://www.w3.org/2001/XMLSchema-instance"),
            (object)new XAttribute(val + "schemaLocation", (object)"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
            allSiteMapEntries.Select((Func<SiteMapEntry, XElement>)((SiteMapEntry u) => new XElement(ns + "url", (object)new XElement(ns + "loc", (object)u.Uri.ToString().Replace("http//", "http://")))))
        });
        _siteMapView.SiteMapEntries = ((object)val2).ToString();
    }
}
