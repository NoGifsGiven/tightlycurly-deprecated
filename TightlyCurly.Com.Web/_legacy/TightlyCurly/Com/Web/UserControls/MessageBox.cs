using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace TightlyCurly.Com.Web.UserControls;

public class MessageBox : UserControl
{
    protected Panel pnlMessageBox;

    protected HtmlTableRow titleRow;

    protected Label lblTitle;

    protected HiddenField close;

    protected Label lblMessage;

    protected ModalPopupExtender messageBoxPopUp;

    public string Title
    {
        private get
        {
            return lblTitle.Text;
        }
        set
        {
            lblTitle.Text = value;
        }
    }

    public string Message
    {
        set
        {
            lblMessage.Text = value;
        }
    }

    public void Show()
    {
        ((Control)lblTitle).Visible = !string.IsNullOrEmpty(Title);
        messageBoxPopUp.Show();
    }

    public void Show(string message)
    {
        Show(string.Empty, message);
    }

    public void Show(string title, string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentNullException("message");
        }
        Title = title;
        Message = message;
        Show();
    }
}
