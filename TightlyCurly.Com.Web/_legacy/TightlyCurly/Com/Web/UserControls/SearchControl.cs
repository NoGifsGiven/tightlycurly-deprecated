using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Resources.TightlyCurly.Com;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Framework.Web.Utilities;

namespace TightlyCurly.Com.Web.UserControls;

public class SearchControl : UserControl
{
    protected Panel SearchPanel;

    protected Label searchLabel;

    protected TextBox searchText;

    protected RequiredFieldValidator searchValidator;

    protected Button searchButton;

    protected Panel panel;

    protected AutoCompleteExtender searchAutoComplete;

    public string CssClass
    {
        set
        {
            ((WebControl)searchLabel).CssClass = value;
        }
    }

    public string LabelText
    {
        set
        {
            searchLabel.Text = value;
        }
    }

    public string TextCssClass
    {
        set
        {
            ((WebControl)searchText).CssClass = value;
        }
    }

    public string Text
    {
        set
        {
            searchText.Text = value;
        }
    }

    public string ButtonCssClass
    {
        set
        {
            ((WebControl)searchButton).CssClass = value;
        }
    }

    public string ButtonText
    {
        set
        {
            searchButton.Text = value;
        }
    }

    public bool CausesValidation
    {
        set
        {
            ((WebControl)searchValidator).Enabled = value;
        }
    }

    public string ErrorMessage
    {
        set
        {
            ((BaseValidator)searchValidator).ErrorMessage = value;
        }
    }

    public string ValidationGroup
    {
        set
        {
            searchText.ValidationGroup = value;
            ((BaseValidator)searchValidator).ValidationGroup = value;
            searchButton.ValidationGroup = value;
        }
    }

    public Unit TextWidth
    {
        set
        {
            //IL_0007: Unknown result type (might be due to invalid IL or missing references)
            ((WebControl)searchText).Width = value;
        }
    }

    public string ServicePath
    {
        set
        {
            searchAutoComplete.ServicePath = value;
        }
    }

    public string ServiceMethod
    {
        set
        {
            searchAutoComplete.ServiceMethod = value;
        }
    }

    public bool AutoCompleteEnabled
    {
        set
        {
            ((Control)panel).Visible = value;
            searchAutoComplete.Enabled = value;
        }
    }

    private string SearchTextValue
    {
        get
        {
            return TextEncoder.SafeEncode(searchText.Text);
        }
        set
        {
            searchText.Text = value;
        }
    }

    public MessageBox MessageBox => ((Control)((Control)this).Page.Master).FindControl("MessageBoxControl") as MessageBox;

    public event EventHandler<SearchEventArgs> SearchSelected;

    protected override void OnLoad(EventArgs e)
    {
        SearchPanel.DefaultButton = ((Control)searchButton).ID;
        ((Control)this).OnLoad(e);
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        string searchTextValue = SearchTextValue;
        if (!string.IsNullOrEmpty(searchTextValue))
        {
            OnSearched(searchTextValue);
            SearchTextValue = string.Empty;
        }
        else
        {
            MessageBox.Show(Resources.TightlyCurly.Com.Web.noSearchTermErrorMessage);
        }
    }

    private void OnSearched(string searchValue)
    {
        if (this.SearchSelected != null)
        {
            this.SearchSelected(this, new SearchEventArgs(searchValue));
        }
    }
}
