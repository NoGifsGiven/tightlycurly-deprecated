using System;
using System.Collections.Generic;
using System.Linq;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Extensions;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public class SiteMapDataProvider : ISiteMapDataProvider
{
    protected readonly IUrlEntryDataProvider _urlEntryDataProvider;

    protected readonly IModelConverter<UrlEntry, SiteMapEntry> _modelConverter;

    public SiteMapDataProvider(IUrlEntryDataProvider urlEntryDataProvider, IModelConverter<UrlEntry, SiteMapEntry> modelConverter)
    {
        if (urlEntryDataProvider == null)
        {
            throw new ArgumentNullException("urlEntryDataProvider");
        }
        if (modelConverter == null)
        {
            throw new ArgumentNullException("modelConverter");
        }
        _urlEntryDataProvider = urlEntryDataProvider;
        _modelConverter = modelConverter;
    }

    public IEnumerable<SiteMapEntry> GetAllSiteMapEntries()
    {
        IEnumerable<UrlEntry> allUrlEntries = _urlEntryDataProvider.GetAllUrlEntries();
        return allUrlEntries.IsNullOrEmpty() ? Enumerable.Empty<SiteMapEntry>() : allUrlEntries.Select((UrlEntry u) => _modelConverter.Convert(u));
    }
}
