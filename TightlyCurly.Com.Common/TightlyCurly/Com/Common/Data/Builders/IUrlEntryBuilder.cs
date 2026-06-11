using System;
using System.Collections.Generic;
using System.Data;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.Builders;

public interface IUrlEntryBuilder
{
    IEnumerable<UrlEntry> BuildUrlEntries(Func<IDataReader> readerDelegate);

    IEnumerable<UrlRoute> BuildUrlRoutes(Func<IDataReader> readerDelegate);

    UrlEntry LoadUrlEntryFromReader(IDataReader reader);

    UrlRoute LoadUrlRouteFromReader(IDataReader reader);
}
