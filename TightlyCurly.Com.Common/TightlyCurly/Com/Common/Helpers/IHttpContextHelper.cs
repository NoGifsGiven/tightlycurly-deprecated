using System.Collections.Specialized;

namespace TightlyCurly.Com.Common.Helpers;

public interface IHttpContextHelper
{
    NameValueCollection QueryString { get; }

    void SetStatusCode(int statusCode);

    void SetMimeType(string mimeType);
}
