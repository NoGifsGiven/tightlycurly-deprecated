namespace TightlyCurly.Com.Framework.Web.Utilities;

/// <summary>
/// Web Forms-era script helpers, reworked for ASP.NET Core: instead of mutating
/// server controls (System.Web.UI no longer exists), these build the equivalent
/// markup/attribute strings for use in Razor views.
/// </summary>
public static class ScriptUtilities
{
    /// <summary>
    /// Builds the onclick attribute value that shows a JavaScript confirm box.
    /// </summary>
    public static string BuildConfirmBoxAttribute(string message)
    {
        return $"return confirm('{message}');";
    }

    /// <summary>
    /// Builds a script block that shows a JavaScript alert box.
    /// </summary>
    public static string BuildAlertBoxScript(string alertMessage)
    {
        if (string.IsNullOrEmpty(alertMessage))
        {
            return string.Empty;
        }
        return $"<script type=\"text/javascript\">alert('{alertMessage}');</script>";
    }
}
