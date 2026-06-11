using Microsoft.AspNetCore.Mvc.ApplicationModels;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.Builders;
using TightlyCurly.Com.Common.Data.DataAccess;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Data.Mappers;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Web.Services;

namespace TightlyCurly.Com.Web.Helpers;

public static class Bootstrapper
{
    /// <summary>
    /// Maps the legacy *.aspx handler paths stored in the UrlRoutes table to the
    /// Razor Pages that replace them.
    /// </summary>
    private static readonly Dictionary<string, string> HandlerPathToPage = new(StringComparer.OrdinalIgnoreCase)
    {
        ["ContentPage"] = "/Content",
        ["AnswerBank"] = "/AnswerBank",
        ["Ingredients"] = "/Ingredients",
        ["Contact"] = "/Contact",
        ["SiteMapView"] = "/SiteMap",
        ["SiteMap"] = "/SiteMap",
        ["Default"] = "/Index",
        ["_Default"] = "/Index"
    };

    /// <summary>
    /// Replaces Global.asax Application_Start: the type graph formerly registered in
    /// the legacy static Castle Windsor container, now in the standard DI container.
    /// Transient lifetimes match the original Windsor LifestyleTransient registrations.
    /// </summary>
    public static IServiceCollection AddTightlyCurlyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfigurationHelper>(new AppSettingsConfigurationHelper(configuration));
        services.AddTransient<IParameterMapper, SqlParameterMapper>();
        services.AddTransient<IQuestionDataProvider, SqlQuestionDataProvider>();
        services.AddTransient<ISettingsDataProvider, SqlSettingsDataProvider>();
        services.AddTransient<ISettingsHelper, SettingsHelper>();
        services.AddTransient<ISiteMapWrapper, SiteMapWrapper>();
        services.AddTransient<IEmailProvider, SmtpEmailProvider>();
        services.AddTransient<IHttpContextHelper, HttpContextHelper>();
        services.AddTransient<IResourceHelper, ResourceHelper>();
        services.AddTransient<ICacheHelper, HttpCacheHelper>();
        services.AddTransient<IPageService, PageService>();
        services.AddTransient<IUrlEntryBuilder, UrlEntryBuilder>();
        services.AddTransient<IUrlEntryDataAccess, SqlUrlEntryDataAccess>();
        services.AddTransient<IUrlEntryDataProvider, SqlUrlEntryDataProvider>();
        services.AddTransient<IPageBuilder, PageBuilder>();
        services.AddTransient<IPageDataAccess, SqlPageDataAccess>();
        services.AddTransient<IPageDataProvider, SqlPageDataProvider>();
        services.AddTransient<IIngredientDataProvider, DapperIngredientDataProvider>();
        services.AddTransient<IUserDataProvider, DapperUserDataProvider>();
        services.AddTransient<IModelConverter<UrlEntry>, UriModelConverter>();
        services.AddTransient<IFormatter<string, string>, SeoFriendlyFormatter>();
        services.AddTransient<IModelConverter<UrlEntry, SiteMapEntry>, SiteMapModelConverter>();
        services.AddTransient<ISiteMapDataProvider, SiteMapDataProvider>();
        services.AddTransient<AutoCompleteService>();
        return services;
    }

    /// <summary>
    /// Replaces RegisterRoutes(RouteCollection): loads the dynamic routes from the
    /// database and registers them as additional Razor Page routes.
    /// </summary>
    public static void AddDynamicPageRoutes(PageConventionCollection conventions, IUrlEntryDataProvider urlEntryDataProvider, ILogger logger = null)
    {
        try
        {
            foreach (UrlRoute urlRoute in urlEntryDataProvider.GetAllUrlRoutes())
            {
                string page = TranslateHandlerPath(urlRoute.HandlerPath);
                if (page != null && !string.IsNullOrEmpty(urlRoute.RouteUrl))
                {
                    conventions.AddPageRoute(page, urlRoute.RouteUrl);
                }
            }
        }
        catch (Exception ex)
        {
            // The site must still start when the database is unreachable; the static
            // page routes keep the core pages working.
            logger?.LogWarning(ex, "Could not load dynamic page routes from the database.");
        }
    }

    private static string TranslateHandlerPath(string handlerPath)
    {
        if (string.IsNullOrEmpty(handlerPath))
        {
            return null;
        }
        string name = handlerPath
            .Replace("~/", string.Empty)
            .Replace(".aspx", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Trim('/');
        return HandlerPathToPage.TryGetValue(name, out string page) ? page : null;
    }
}
