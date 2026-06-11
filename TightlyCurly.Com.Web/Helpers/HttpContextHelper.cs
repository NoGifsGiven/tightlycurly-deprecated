using System.Collections.Specialized;
using TightlyCurly.Com.Common.Helpers;

namespace TightlyCurly.Com.Web.Helpers;

/// <summary>
/// ASP.NET Core implementation of IHttpContextHelper (formerly backed by
/// System.Web.HttpContext.Current).
/// </summary>
public class HttpContextHelper : IHttpContextHelper
{
    // HttpContextAccessor is backed by an AsyncLocal, so a private instance observes
    // the ambient request context without requiring constructor injection.
    private static readonly HttpContextAccessor Accessor = new();

    private static HttpContext Current => Accessor.HttpContext;

    public NameValueCollection QueryString
    {
        get
        {
            var values = new NameValueCollection();
            var context = Current;
            if (context != null)
            {
                foreach (var pair in context.Request.Query)
                {
                    foreach (var value in pair.Value)
                    {
                        values.Add(pair.Key, value);
                    }
                }
            }
            return values;
        }
    }

    public void SetStatusCode(int statusCode)
    {
        var context = Current;
        if (context != null && !context.Response.HasStarted)
        {
            context.Response.StatusCode = statusCode;
        }
    }

    public void SetMimeType(string mimeType)
    {
        var context = Current;
        if (context != null && !context.Response.HasStarted)
        {
            context.Response.ContentType = mimeType;
        }
    }
}
