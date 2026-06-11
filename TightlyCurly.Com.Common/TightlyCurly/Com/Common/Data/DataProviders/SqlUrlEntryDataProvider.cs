using System;
using System.Collections.Generic;
using System.Linq;
using TightlyCurly.Com.Common.Data.Builders;
using TightlyCurly.Com.Common.Data.DataAccess;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public class SqlUrlEntryDataProvider : IUrlEntryDataProvider
{
    protected readonly IUrlEntryDataAccess _urlEntryDataAccess;

    protected readonly IUrlEntryBuilder _urlEntryHelper;

    public SqlUrlEntryDataProvider(IUrlEntryDataAccess urlEntryDataAccess, IUrlEntryBuilder urlEntryHelper)
    {
        if (urlEntryDataAccess == null)
        {
            throw new ArgumentNullException("urlEntryDataAccess");
        }
        if (urlEntryHelper == null)
        {
            throw new ArgumentNullException("urlEntryHelper");
        }
        _urlEntryDataAccess = urlEntryDataAccess;
        _urlEntryHelper = urlEntryHelper;
    }

    public IEnumerable<UrlRoute> GetAllUrlRoutes()
    {
        return _urlEntryHelper.BuildUrlRoutes(() => _urlEntryDataAccess.GetAllUrlRoutes());
    }

    public UrlRoute GetUrlRouteById(int urlRouteId)
    {
        if (urlRouteId <= 0)
        {
            throw new ArgumentException("urlRouteId cannot be less than or equal to zero.", "urlRouteId");
        }
        return _urlEntryHelper.BuildUrlRoutes(() => _urlEntryDataAccess.GetUrlRouteById(urlRouteId)).FirstOrDefault();
    }

    public UrlRoute GetUrlRouteByRouteType(RouteType routeType)
    {
        if (routeType == (RouteType)0)
        {
            throw new ArgumentException("routeType cannot be zero.", "routeType");
        }
        return _urlEntryHelper.BuildUrlRoutes(() => _urlEntryDataAccess.GetUrlRouteByRouteType(routeType)).FirstOrDefault();
    }

    public UrlRoute SaveUrlRoute(UrlRoute urlRoute)
    {
        if (urlRoute == null)
        {
            throw new ArgumentNullException("urlRoute");
        }
        return _urlEntryHelper.BuildUrlRoutes(() => _urlEntryDataAccess.SaveUrlRoute(urlRoute)).FirstOrDefault();
    }

    public void DeleteUrlRoute(int urlRouteId)
    {
        if (urlRouteId <= 0)
        {
            throw new ArgumentException("urlRouteId cannot be less than or equal to zero.", "urlRouteId");
        }
        _urlEntryDataAccess.DeleteUrlRoute(urlRouteId);
    }

    public IEnumerable<UrlEntry> GetAllUrlEntries()
    {
        return _urlEntryHelper.BuildUrlEntries(() => _urlEntryDataAccess.GetAllUrlEntries());
    }

    public UrlEntry GetUrlEntryById(int urlEntryId)
    {
        if (urlEntryId <= 0)
        {
            throw new ArgumentException("urlEntryId cannot be less than or equal to zero.", "urlEntryId");
        }
        return _urlEntryHelper.BuildUrlEntries(() => _urlEntryDataAccess.GetUrlEntryById(urlEntryId)).FirstOrDefault();
    }

    public UrlEntry GetUrlEntryByKey(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("key");
        }
        return _urlEntryHelper.BuildUrlEntries(() => _urlEntryDataAccess.GetUrlEntryByKey(key)).FirstOrDefault();
    }

    public UrlEntry GetUrlEntryByObjectTypeAndObjectId(ObjectType objectType, int objectId)
    {
        if (objectType == (ObjectType)0)
        {
            throw new ArgumentException("objectType cannot be zero.", "objectType");
        }
        if (objectId <= 0)
        {
            throw new ArgumentException("objectId cannot be less than or equal to zero.", "objectId");
        }
        return _urlEntryHelper.BuildUrlEntries(() => _urlEntryDataAccess.GetUrlEntryByObjectTypeAndId(objectType, objectId)).FirstOrDefault();
    }

    public UrlEntry SaveUrlEntry(UrlEntry urlEntry)
    {
        if (urlEntry == null)
        {
            throw new ArgumentNullException("urlEntry");
        }
        return _urlEntryHelper.BuildUrlEntries(() => _urlEntryDataAccess.SaveUrlEntry(urlEntry)).FirstOrDefault();
    }

    public void DeleteUrlEntry(int urlEntryId)
    {
        if (urlEntryId <= 0)
        {
            throw new ArgumentException("urlEntryId cannot be less than or equal to zero.", "urlEntryId");
        }
        _urlEntryDataAccess.DeleteUrlEntry(urlEntryId);
    }

    public IEnumerable<UrlEntry> GetUrlEntriesByUrlRoute(UrlRoute urlRoute)
    {
        if (urlRoute == null)
        {
            throw new ArgumentNullException("urlRoute");
        }
        return _urlEntryHelper.BuildUrlEntries(() => _urlEntryDataAccess.GetUrlEntriesByUrlRoute(urlRoute));
    }
}
