using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace TightlyCurly.Com.Web;

public class _Default : Page
{
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Page)this).Response.RedirectToRoute("Content Route", (object)new
        {
            pageName = "welcome"
        });
    }
}
