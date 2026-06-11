using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Utilities;

namespace TightlyCurly.Com.Common.Data.Builders;

public class UrlEntryBuilder : IUrlEntryBuilder
{
    public IEnumerable<UrlEntry> BuildUrlEntries(Func<IDataReader> readerDelegate)
    {
        List<UrlEntry> list = new List<UrlEntry>();
        using (IDataReader dataReader = readerDelegate())
        {
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    list.Add(LoadUrlEntryFromReader(dataReader));
                }
            }
        }
        return list;
    }

    public IEnumerable<UrlRoute> BuildUrlRoutes(Func<IDataReader> readerDelegate)
    {
        Dictionary<int, UrlRoute> dictionary = new Dictionary<int, UrlRoute>();
        IDataReader reader = readerDelegate();
        try
        {
            while (reader.Read())
            {
                int key = (int)reader["UrlRouteId"];
                UrlRoute urlRoute;
                if (dictionary.ContainsKey(key))
                {
                    urlRoute = dictionary[key];
                }
                else
                {
                    urlRoute = LoadUrlRouteFromReader(reader);
                    dictionary.Add(urlRoute.UrlRouteId, urlRoute);
                }
                if (reader["urlRouteId"] != DBNull.Value && urlRoute.UrlEntries.SingleOrDefault((UrlEntry u) => u.UrlEntryId == (int)reader["urlEntryId"]) == null)
                {
                    List<UrlEntry> list = urlRoute.UrlEntries.ToList();
                    list.Add(LoadUrlEntryFromReader(reader));
                    urlRoute.UrlEntries = list;
                }
                dictionary[key] = urlRoute;
            }
        }
        finally
        {
            if (reader != null)
            {
                reader.Dispose();
            }
        }
        return dictionary.Values.Select((UrlRoute u) => u);
    }

    public UrlRoute LoadUrlRouteFromReader(IDataReader reader)
    {
        UrlRoute urlRoute = new UrlRoute();
        urlRoute.UrlRouteId = int.Parse(reader["UrlRouteId"].ToString());
        urlRoute.RouteName = reader["RouteName"].ToString();
        urlRoute.RouteType = ((reader["RouteType"] != DBNull.Value) ? EnumParser.Parse<RouteType>(reader["RouteType"].ToString()) : ((RouteType)0));
        urlRoute.RouteUrl = reader["RouteUrl"].ToString();
        urlRoute.HandlerPath = reader["HandlerPath"].ToString();
        urlRoute.EnteredDate = DateTimeOffset.Parse(reader["UrlRouteEnteredDate"].ToString());
        urlRoute.UpdatedDate = DateTimeOffset.Parse(reader["UrlRouteUpdatedDate"].ToString());
        return urlRoute;
    }

    public UrlEntry LoadUrlEntryFromReader(IDataReader reader)
    {
        object result;
        if (!Convert.IsDBNull(reader["UrlEntryId"]))
        {
            UrlEntry urlEntry = new UrlEntry();
            urlEntry.UrlEntryId = int.Parse(reader["UrlEntryId"].ToString());
            urlEntry.Key = reader["UrlEntryKey"].ToString();
            urlEntry.Priority = reader["UrlEntryPriority"].ToString();
            urlEntry.Uri = new Uri(reader["UrlEntryUri"].ToString(), UriKind.RelativeOrAbsolute);
            urlEntry.ChangeFrequency = EnumParser.Parse<ChangeFrequency>(reader["UrlEntryChangeFrequency"].ToString());
            urlEntry.EnteredDate = DateTimeOffset.Parse(reader["UrlEntryEnteredDate"].ToString());
            urlEntry.UpdatedDate = DateTimeOffset.Parse(reader["UrlEntryUpdatedDate"].ToString());
            urlEntry.ObjectType = ((reader["UrlEntryObjectType"] == DBNull.Value) ? ObjectType.None : EnumParser.Parse<ObjectType>(reader["UrlEntryObjectType"].ToString()));
            urlEntry.ObjectId = ((reader["UrlEntryObjectId"] != DBNull.Value) ? int.Parse(reader["UrlEntryObjectId"].ToString()) : 0);
            result = urlEntry;
        }
        else
        {
            result = null;
        }
        return (UrlEntry)result;
    }
}
