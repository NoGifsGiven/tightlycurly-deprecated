using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface IUrlEntryDataProvider
{
    IEnumerable<UrlRoute> GetAllUrlRoutes();

    UrlRoute GetUrlRouteById(int urlRouteId);

    UrlRoute GetUrlRouteByRouteType(RouteType routeType);

    UrlRoute SaveUrlRoute(UrlRoute urlRoute);

    void DeleteUrlRoute(int urlRouteId);

    IEnumerable<UrlEntry> GetAllUrlEntries();

    UrlEntry GetUrlEntryById(int urlEntryId);

    UrlEntry GetUrlEntryByKey(string key);

    UrlEntry GetUrlEntryByObjectTypeAndObjectId(ObjectType objectType, int objectId);

    UrlEntry SaveUrlEntry(UrlEntry urlEntry);

    void DeleteUrlEntry(int urlEntryId);

    IEnumerable<UrlEntry> GetUrlEntriesByUrlRoute(UrlRoute urlRoute);
}
