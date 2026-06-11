using System.Configuration;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Utilities;

namespace TightlyCurly.Com.Common.Helpers;

public class ConfigurationHelper : IConfigurationHelper
{
    public string DefaultConnectionString => ConfigurationManager.ConnectionStrings["TightlyCurly"].ConnectionString;

    public string AdminConnectionString => ConfigurationManager.ConnectionStrings["ALLIGOSHEE_HAIR_ADMINConnectionString"].ConnectionString;

    public string ContentService => ConfigurationManager.ConnectionStrings["ContentService"].ConnectionString;

    public string ProductService => ConfigurationManager.ConnectionStrings["ProductService"].ConnectionString;

    public string AutoCompleteService => ConfigurationManager.ConnectionStrings["AutoCompleteService"].ConnectionString;

    public bool EnableAnalytics => bool.Parse(ConfigurationManager.AppSettings["enableAnalytics"]);

    public string IngredientsNameListServiceMethod => ConfigurationManager.AppSettings["IngredientsNameListServiceMethod"];

    public string QuestionListServiceMethod => ConfigurationManager.AppSettings["QuestionListServiceMethod"];

    public double DefaultCacheExpiration
    {
        get
        {
            double.TryParse(ConfigurationManager.AppSettings["DefaultCacheExpiration"], out var result);
            return result;
        }
    }

    public bool CacheEnabled => bool.Parse(ConfigurationManager.AppSettings["CacheEnabled"]);

    public string IngredientsUrlPrefix => ConfigurationManager.AppSettings["ingredientsUrlPrefix"];

    public string IngredientsUrlPriority => ConfigurationManager.AppSettings["ingredientsUrlPriority"];

    public ChangeFrequency IngredientsUrlChangeFrequency => EnumParser.Parse<ChangeFrequency>(ConfigurationManager.AppSettings["ingredientsUrlChangeFrequency"]);

    public string BaseSiteUrl => ConfigurationManager.AppSettings["baseSiteUrl"];

    public string QuestionsUrlPrefix => ConfigurationManager.AppSettings["questionsUrlPrefix"];

    public string QuestionsUrlPriority => ConfigurationManager.AppSettings["questionsUrlPriority"];

    public ChangeFrequency QuestionsUrlChangeFrequency => EnumParser.Parse<ChangeFrequency>(ConfigurationManager.AppSettings["questionsUrlChangeFrequency"]);

    public string DefaultPage => ConfigurationManager.AppSettings["defaultPage"];
}
