namespace TightlyCurly.Com.Web.Presenters;

public interface IContactView
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string EmailAddress { get; set; }

    string Comments { get; set; }

    bool AddToBookUpdates { get; set; }

    bool Enabled { get; set; }

    string DisabledMessage { get; set; }
}
