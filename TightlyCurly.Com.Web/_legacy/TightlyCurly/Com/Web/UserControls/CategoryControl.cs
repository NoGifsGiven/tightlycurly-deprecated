using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Web.Configuration;

namespace TightlyCurly.Com.Web.UserControls;

public class CategoryControl : UserControl
{
    private CategoryElementCollection _categories;

    public CategoryElementCollection Categories
    {
        get
        {
            return _categories;
        }
        set
        {
            _categories = value;
        }
    }

    public event EventHandler<ItemSelectedEventArgs> ItemSelected;

    protected override void OnLoad(EventArgs e)
    {
        //IL_001d: Unknown result type (might be due to invalid IL or missing references)
        //IL_0023: Expected O, but got Unknown
        //IL_0075: Unknown result type (might be due to invalid IL or missing references)
        //IL_007f: Expected O, but got Unknown
        foreach (Category item in (ConfigurationElementCollection)Categories)
        {
            LinkButton val = new LinkButton();
            ((Control)val).ID = item.CategoryName;
            val.Text = item.CategoryName;
            val.Click += SelectedClicked;
            val.CommandArgument = item.Arguments;
            ((Control)this).Controls.Add((Control)(object)val);
            ((Control)this).Controls.Add((Control)new LiteralControl("&nbsp;"));
        }
        ((Control)this).OnLoad(e);
    }

    protected void SelectedClicked(object sender, EventArgs e)
    {
        LinkButton val = (LinkButton)((sender is LinkButton) ? sender : null);
        if (val != null)
        {
            OnItemSelected(val.CommandArgument);
        }
    }

    private void OnItemSelected(string selectedItem)
    {
        if (this.ItemSelected != null)
        {
            this.ItemSelected(this, new ItemSelectedEventArgs(selectedItem));
        }
    }
}
