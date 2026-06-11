using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Resources.TightlyCurly.Com;
using TightlyCurly.Com.Web.MasterPages;
using TightlyCurly.Com.Web.UserControls;

namespace TightlyCurly.Com.Web;

public class Ingredients : Page
{
    protected HtmlGenericControl blurb;

    public Panel Preamble;

    protected IngredientsControl ingredients;

    protected override void OnLoad(EventArgs e)
    {
        if (((Page)this).Master is Master master)
        {
            master.PageTitle = Resources.TightlyCurly.Com.Web.ingredientsDescription;
        }
        ((Control)this).OnLoad(e);
    }
}
