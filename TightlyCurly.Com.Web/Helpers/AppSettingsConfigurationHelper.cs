using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Utilities;

namespace TightlyCurly.Com.Web.Helpers;

/// <summary>
/// IConfigurationHelper backed by ASP.NET Core configuration (appsettings.json),
/// replacing the web.config-based ConfigurationHelper.
/// </summary>
public class AppSettingsConfigurationHelper : IConfigurationHelper
{
    private readonly IConfiguration _configuration;

    public AppSettingsConfigurationHelper(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        _configuration = configuration;
    }

    public string DefaultConnectionString => _configuration.GetConnectionString("TightlyCurly");

    public string AdminConnectionString => _configuration.GetConnectionString("ALLIGOSHEE_HAIR_ADMINConnectionString");

    public string ContentService => _configuration.GetConnectionString("ContentService");

    public string ProductService => _configuration.GetConnectionString("ProductService");

    public string AutoCompleteService => _configuration.GetConnectionString("AutoCompleteService");

    public bool EnableAnalytics => GetBool("enableAnalytics");

    public string IngredientsNameListServiceMethod => GetString("IngredientsNameListServiceMethod");

    public string QuestionListServiceMethod => GetString("QuestionListServiceMethod");

    public double DefaultCacheExpiration
    {
        get
        {
            double.TryParse(GetString("DefaultCacheExpiration"), out var result);
            return result;
        }
    }

    public bool CacheEnabled => GetBool("CacheEnabled");

    public string IngredientsUrlPrefix => GetString("ingredientsUrlPrefix");

    public string IngredientsUrlPriority => GetString("ingredientsUrlPriority");

    public ChangeFrequency IngredientsUrlChangeFrequency => GetChangeFrequency("ingredientsUrlChangeFrequency");

    public string BaseSiteUrl => GetString("baseSiteUrl");

    public string QuestionsUrlPrefix => GetString("questionsUrlPrefix");

    public string QuestionsUrlPriority => GetString("questionsUrlPriority");

    public ChangeFrequency QuestionsUrlChangeFrequency => GetChangeFrequency("questionsUrlChangeFrequency");

    public string DefaultPage => GetString("defaultPage");

    private string GetString(string key)
    {
        return _configuration[$"AppSettings:{key}"];
    }

    private bool GetBool(string key)
    {
        return bool.TryParse(GetString(key), out var result) && result;
    }

    private ChangeFrequency GetChangeFrequency(string key)
    {
        var value = GetString(key);
        return string.IsNullOrEmpty(value) ? ChangeFrequency.Monthly : EnumParser.Parse<ChangeFrequency>(value);
    }
}
