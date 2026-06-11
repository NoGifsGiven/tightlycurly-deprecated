using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TightlyCurly.Com.Common.Data.Mappers;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Extensions;

namespace TightlyCurly.Com.Common.Data.DataAccess;

public class SqlUrlEntryDataAccess : SqlDataAccessBase, IUrlEntryDataAccess
{
    public SqlUrlEntryDataAccess(IConfigurationHelper configurationHelper, IParameterMapper parameterMapper)
        : base(configurationHelper, parameterMapper)
    {
    }

    public IDataReader GetAllUrlEntries()
    {
        return ExecuteDataReader("GetAllUrlEntries", null);
    }

    public IDataReader GetUrlEntryById(int urlEntryId)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "urlEntryId",
            Value = urlEntryId
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("GetUrlEntryById", namedParameters);
    }

    public IDataReader GetUrlEntryByKey(string key)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "key",
            Value = key
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("GetUrlEntryByKey", namedParameters);
    }

    public IDataReader SaveUrlEntry(UrlEntry urlEntry)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "urlEntryId",
            Value = urlEntry.UrlEntryId
        });
        list.Add(new NamedParameter
        {
            Name = "key",
            Value = urlEntry.Key
        });
        list.Add(new NamedParameter
        {
            Name = "uri",
            Value = urlEntry.Uri.ToString()
        });
        list.Add(new NamedParameter
        {
            Name = "priority",
            Value = urlEntry.Priority
        });
        list.Add(new NamedParameter
        {
            Name = "changeFrequency",
            Value = (int)urlEntry.ChangeFrequency
        });
        list.Add(new NamedParameter
        {
            Name = "objectType",
            Value = ((urlEntry.ObjectType == ObjectType.None) ? DBNull.Value : ((object)(int)urlEntry.ObjectType))
        });
        list.Add(new NamedParameter
        {
            Name = "objectId",
            Value = ((urlEntry.ObjectId == 0) ? DBNull.Value : ((object)urlEntry.ObjectId))
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("UpdateUrlEntry", namedParameters);
    }

    public IDataReader GetUrlEntryByObjectTypeAndId(ObjectType objectType, int objectId)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "objectType",
            Value = objectType
        });
        list.Add(new NamedParameter
        {
            Name = "objectId",
            Value = objectId
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("GetUrlEntryByObjectTypeAndObjectId", namedParameters);
    }

    public IDataReader GetUrlRouteByRouteType(RouteType routeType)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "routeType",
            Value = routeType.ToInt()
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("GetUrlRouteByRouteType", namedParameters);
    }

    public void DeleteUrlEntry(int urlEntryId)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "urlEntryId",
            Value = urlEntryId
        });
        List<NamedParameter> namedParameters = list;
        ExecuteNonQuery("DeleteUrlEntryById", namedParameters);
    }

    public IDataReader GetAllUrlRoutes()
    {
        return ExecuteDataReader("GetAllUrlRoutes");
    }

    public IDataReader GetUrlRouteById(int urlRouteId)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "urlRouteId",
            Value = urlRouteId
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("GetUrlRouteById", namedParameters);
    }

    public IDataReader SaveUrlRoute(UrlRoute urlRoute)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "urlRouteId",
            Value = urlRoute.UrlRouteId
        });
        list.Add(new NamedParameter
        {
            Name = "routeName",
            Value = urlRoute.RouteName
        });
        list.Add(new NamedParameter
        {
            Name = "routeUrl",
            Value = urlRoute.RouteUrl
        });
        list.Add(new NamedParameter
        {
            Name = "handlerPath",
            Value = urlRoute.HandlerPath
        });
        list.Add(new NamedParameter
        {
            Name = "routeType",
            Value = urlRoute.RouteType.ToInt()
        });
        list.Add(new NamedParameter
        {
            Name = "urlEntryIds",
            Value = (urlRoute.UrlEntries.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)urlRoute.UrlEntries.Select((UrlEntry u) => u.UrlEntryId).ToDelimitedString(",")))
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("UpdateUrlRoute", namedParameters);
    }

    public void DeleteUrlRoute(int urlRouteId)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "urlRouteId",
            Value = urlRouteId
        });
        List<NamedParameter> namedParameters = list;
        ExecuteNonQuery("DeleteUrlRouteById", namedParameters);
    }

    public IDataReader GetUrlEntriesByUrlRoute(UrlRoute urlRoute)
    {
        List<NamedParameter> list = new List<NamedParameter>();
        list.Add(new NamedParameter
        {
            Name = "urlRouteId",
            Value = urlRoute.UrlRouteId
        });
        List<NamedParameter> namedParameters = list;
        return ExecuteDataReader("GetUrlEntriesByUrlRouteId", namedParameters);
    }
}
