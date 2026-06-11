using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Framework.Net;
using TightlyCurly.Com.Framework.Utilities;
using TightlyCurly.Com.Framework.Web.Utilities;
using TightlyCurly.Com.Web.Presenters;

namespace TightlyCurly.Com.Web.Pages;

/// <summary>Replaces Contact.aspx (the IContactView).</summary>
public class ContactModel : PageModel, IContactView
{
    private readonly ISettingsHelper _settingsHelper;

    private readonly IEmailProvider _emailProvider;

    public ContactModel(ISettingsHelper settingsHelper, IEmailProvider emailProvider)
    {
        _settingsHelper = settingsHelper ?? throw new ArgumentNullException(nameof(settingsHelper));
        _emailProvider = emailProvider ?? throw new ArgumentNullException(nameof(emailProvider));
    }

    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    [BindProperty]
    public string EmailAddress { get; set; }

    [BindProperty]
    public string Comments { get; set; }

    [BindProperty]
    public bool AddToBookUpdates { get; set; }

    public bool Enabled { get; set; }

    public string DisabledMessage { get; set; }

    public void OnGet()
    {
        ViewData["Title"] = "Contact";
        LoadSettings();
    }

    public IActionResult OnPost()
    {
        ViewData["Title"] = "Contact";
        LoadSettings();
        if (!Enabled)
        {
            return Page();
        }

        if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
            string.IsNullOrEmpty(Comments) || string.IsNullOrEmpty(EmailAddress))
        {
            ModelState.AddModelError(string.Empty, "All fields are required.");
            return Page();
        }
        if (!EmailAddress.IsEmailAddress())
        {
            ModelState.AddModelError(nameof(EmailAddress), "The email address is invalid.");
            return Page();
        }

        try
        {
            SendComments();
            TempData["MessageBoxText"] = Constants.MessageConstants.ThankYouMessage;
            ResetFields();
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Your message could not be sent. Please try again later.");
        }

        return Page();
    }

    private void LoadSettings()
    {
        try
        {
            Enabled = _settingsHelper.ContactPageEnabled;
            DisabledMessage = _settingsHelper.ContactPageDisabledMessage;
        }
        catch
        {
            Enabled = false;
            DisabledMessage = "The contact page is temporarily unavailable.";
        }
    }

    private void SendComments()
    {
        var credentials = new MessageCredentials(
            _settingsHelper.SmtpServer,
            _settingsHelper.SmtpUsername,
            _settingsHelper.SmtpPassword,
            _settingsHelper.SmtpServerPort,
            _settingsHelper.SmtpUseSsl);

        string body =
            $"From: {TextEncoder.SafeEncode(FirstName)} {TextEncoder.SafeEncode(LastName)}{Environment.NewLine}" +
            $"Email: {TextEncoder.SafeEncode(EmailAddress)}{Environment.NewLine}" +
            $"Sign up for book updates: {(AddToBookUpdates ? "Yes" : "No")}{Environment.NewLine}{Environment.NewLine}" +
            TextEncoder.SafeEncode(Comments);

        var message = new Message(
            _settingsHelper.CommentsMailTo,
            _settingsHelper.MailFrom,
            "TightlyCurly.com contact form",
            body,
            TightlyCurly.Com.Framework.MessageFormat.Text,
            credentials);

        _emailProvider.SendMail(message);
    }

    public void ResetFields()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Comments = string.Empty;
        EmailAddress = string.Empty;
        ModelState.Clear();
    }
}
