using System.Data;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataAccess;

public interface IUrlEntryDataAccess
{
    IDataReader GetAllUrlEntries();

    IDataReader GetUrlEntryById(int urlEntryId);

    IDataReader GetUrlEntryByKey(string key);

    IDataReader SaveUrlEntry(UrlEntry urlEntry);

    IDataReader GetUrlEntryByObjectTypeAndId(ObjectType objectType, int objectId);

    IDataReader GetUrlRouteByRouteType(RouteType routeType);

    void DeleteUrlEntry(int urlEntryId);

    IDataReader GetAllUrlRoutes();

    IDataReader GetUrlRouteById(int urlRouteId);

    IDataReader SaveUrlRoute(UrlRoute urlRoute);

    void DeleteUrlRoute(int urlRouteId);

    IDataReader GetUrlEntriesByUrlRoute(UrlRoute urlRoute);
}
