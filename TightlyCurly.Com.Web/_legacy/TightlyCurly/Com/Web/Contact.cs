using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TightlyCurly.Com.Framework.Web.Utilities;

namespace TightlyCurly.Com.Web;

public class Contact : Page
{
    protected Panel ContactPanel;

    protected Panel MessageContainer;

    protected Literal MessageContainerText;

    protected Panel ContactContainer;

    protected Label FirstNameLabel;

    protected TextBox FirstNameText;

    protected RequiredFieldValidator FirstNameValidator;

    protected Label LastNameLabel;

    protected TextBox LastNameText;

    protected RequiredFieldValidator LastNameValidator;

    protected Label EmailAddressLabel;

    protected TextBox EmailAddressText;

    protected RequiredFieldValidator EmailAddressValidator;

    protected Label CommentsLabel;

    protected TextBox CommentsText;

    protected RequiredFieldValidator CommentsValidator;

    protected Button AddComment;

    protected CheckBox SignUpBookUpdates;

    public string FirstName
    {
        get
        {
            return TextEncoder.SafeEncode(FirstNameText.Text);
        }
        set
        {
            FirstNameText.Text = value;
        }
    }

    public string LastName
    {
        get
        {
            return TextEncoder.SafeEncode(LastNameText.Text);
        }
        set
        {
            LastNameText.Text = value;
        }
    }

    public string EmailAddress
    {
        get
        {
            return TextEncoder.SafeEncode(EmailAddressText.Text);
        }
        set
        {
            EmailAddressText.Text = value;
        }
    }

    public string Comments
    {
        get
        {
            return TextEncoder.SafeEncode(CommentsText.Text);
        }
        set
        {
            CommentsText.Text = value;
        }
    }

    public bool AddToBookUpdates
    {
        get
        {
            return SignUpBookUpdates.Checked;
        }
        set
        {
            SignUpBookUpdates.Checked = value;
        }
    }

    public bool Enabled { get; set; }

    public string DisabledMessage { get; set; }

    protected override void OnLoad(EventArgs e)
    {
        ((Control)this).OnLoad(e);
        ((Control)MessageContainer).Visible = !Enabled;
        ((Control)ContactContainer).Visible = Enabled;
        MessageContainerText.Text = DisabledMessage;
    }

    protected void AddComments_Click(object sender, EventArgs e)
    {
        try
        {
            ResetFields();
        }
        catch (ArgumentException ex)
        {
            if (ex.Message.Contains("The email address is invalid."))
            {
                return;
            }
            throw;
        }
    }

    public void ResetFields()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Comments = string.Empty;
        EmailAddress = string.Empty;
    }
}
