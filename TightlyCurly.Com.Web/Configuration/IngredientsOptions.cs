namespace TightlyCurly.Com.Web.Configuration;

/// <summary>
/// Replaces the web.config "ingredients" custom configuration section
/// (IngredientsConfigurationSection); bound from the "Ingredients" section
/// of appsettings.json.
/// </summary>
public class IngredientsOptions
{
    public const string SectionName = "Ingredients";

    public string AvoidStyle { get; set; } = "avoid";

    public string CautionStyle { get; set; } = "caution";

    public string AcceptableStyle { get; set; } = "acceptable";

    public string GoodStyle { get; set; } = "good";

    public string LinkUrl { get; set; } = "/ingredients";

    public bool AutoCompleteEnabled { get; set; }

    public List<SearchCategory> SearchCategories { get; set; } = new();
}

/// <summary>Replaces the web.config Category configuration element.</summary>
public class SearchCategory
{
    public string CategoryName { get; set; }

    public string Arguments { get; set; }
}
