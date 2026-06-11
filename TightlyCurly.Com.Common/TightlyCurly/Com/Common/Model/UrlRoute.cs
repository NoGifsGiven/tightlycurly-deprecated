using System;
using System.Collections.Generic;

namespace TightlyCurly.Com.Common.Model;

public class UrlRoute
{
    public int UrlRouteId { get; set; }

    public string RouteName { get; set; }

    public RouteType RouteType { get; set; }

    public string RouteUrl { get; set; }

    public string HandlerPath { get; set; }

    public IEnumerable<UrlEntry> UrlEntries { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public UrlRoute()
    {
        UrlEntries = new List<UrlEntry>();
    }
}
