using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface ISiteMapDataProvider
{
    IEnumerable<SiteMapEntry> GetAllSiteMapEntries();
}
