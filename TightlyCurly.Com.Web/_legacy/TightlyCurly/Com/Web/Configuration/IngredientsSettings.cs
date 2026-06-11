namespace TightlyCurly.Com.Web.Configuration;

public static class IngredientsSettings
{
    public static string AvoidStyle => ConfigurationSectionHandler.IngredientsConfigurationSection.AvoidStyle;

    public static string AcceptableStyle => ConfigurationSectionHandler.IngredientsConfigurationSection.AcceptableStyle;

    public static string CautionStyle => ConfigurationSectionHandler.IngredientsConfigurationSection.CautionStyle;

    public static string GoodStyle => ConfigurationSectionHandler.IngredientsConfigurationSection.GoodStyle;

    public static string LinkUrl => ConfigurationSectionHandler.IngredientsConfigurationSection.LinkUrl;

    public static bool AutoCompleteEnabled => ConfigurationSectionHandler.IngredientsConfigurationSection.AutoCompleteEnabled;

    public static CategoryElementCollection SearchCategories => ConfigurationSectionHandler.IngredientsConfigurationSection.SearchCategories.Categories;
}
