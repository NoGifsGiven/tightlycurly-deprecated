using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TightlyCurly.Com.Web.Helpers;

/// <summary>
/// Helpers for Razor Pages that call the API controllers in-process and need the
/// payload (or status code) back out of the ActionResult.
/// </summary>
public static class ControllerResultExtensions
{
    /// <summary>The payload of a successful result (Ok or direct value); null for error results.</summary>
    public static T GetValue<T>(this ActionResult<T> result) where T : class
    {
        return result.Value ?? (result.Result as ObjectResult)?.Value as T;
    }

    /// <summary>The HTTP status code an error result would produce (200 when unset).</summary>
    public static int GetStatusCode<T>(this ActionResult<T> result)
    {
        return (result.Result as IStatusCodeActionResult)?.StatusCode ?? StatusCodes.Status200OK;
    }
}
