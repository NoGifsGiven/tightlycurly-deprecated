using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Helpers;

public interface IConfigurationHelper
{
    string AdminConnectionString { get; }

    string DefaultConnectionString { get; }

    string AutoCompleteService { get; }

    string ContentService { get; }

    string ProductService { get; }

    bool EnableAnalytics { get; }

    string IngredientsNameListServiceMethod { get; }

    string QuestionListServiceMethod { get; }

    double DefaultCacheExpiration { get; }

    bool CacheEnabled { get; }

    string IngredientsUrlPrefix { get; }

    string IngredientsUrlPriority { get; }

    ChangeFrequency IngredientsUrlChangeFrequency { get; }

    string BaseSiteUrl { get; }

    string QuestionsUrlPrefix { get; }

    string QuestionsUrlPriority { get; }

    ChangeFrequency QuestionsUrlChangeFrequency { get; }

    string DefaultPage { get; }
}
