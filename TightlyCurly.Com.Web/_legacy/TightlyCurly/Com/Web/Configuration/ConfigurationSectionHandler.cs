using System.Configuration;

namespace TightlyCurly.Com.Web.Configuration;

public static class ConfigurationSectionHandler
{
    public static IngredientsConfigurationSection IngredientsConfigurationSection => ConfigurationManager.GetSection("ingredients") as IngredientsConfigurationSection;
}
