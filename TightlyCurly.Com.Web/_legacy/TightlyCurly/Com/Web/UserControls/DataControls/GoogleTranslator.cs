using System;
using System.Web.UI;

namespace TightlyCurly.Com.Web.UserControls.DataControls;

public class GoogleTranslator : UserControl
{
    private const string Script = "<script type=\"text/javascript\">function translate(pattern){if(pattern&&pattern!='undefined'){var destination='http://www.google.com/translate?u='+window.location.href+'&langpair='+pattern+'&hl=en&ie=UTF8';window.open(dest,'subwindow','toolbar=yes,location=yes, directories=yes,status=yes,scrollbars=yes,menubar=yes,resizable=yes,left=0,top=0');}}</script>";

    private const string ScriptKey = "translate";

    protected override void OnLoad(EventArgs e)
    {
        if (!((Control)this).Page.ClientScript.IsStartupScriptRegistered("translate"))
        {
            ((Control)this).Page.ClientScript.RegisterStartupScript(((object)((Control)this).Page).GetType(), "translate", "<script type=\"text/javascript\">function translate(pattern){if(pattern&&pattern!='undefined'){var destination='http://www.google.com/translate?u='+window.location.href+'&langpair='+pattern+'&hl=en&ie=UTF8';window.open(dest,'subwindow','toolbar=yes,location=yes, directories=yes,status=yes,scrollbars=yes,menubar=yes,resizable=yes,left=0,top=0');}}</script>");
        }
        ((Control)this).OnLoad(e);
    }
}
