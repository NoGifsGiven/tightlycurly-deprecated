using System.Web.Routing;
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
    public static void Initialize()
    {
        RegisterTypes();
    }

    public static void RegisterRoutes(RouteCollection routes)
    {
        IUrlEntryDataProvider urlEntryDataProvider = Container.Resolve<IUrlEntryDataProvider>();
        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.Ignore("{page}.aspx");
        routes.Ignore("{page}.htm");
        routes.Ignore("{file}.xml");
        foreach (UrlRoute allUrlRoute in urlEntryDataProvider.GetAllUrlRoutes())
        {
            routes.MapPageRoute(allUrlRoute.RouteName, allUrlRoute.RouteUrl, allUrlRoute.HandlerPath);
        }
    }

    private static void RegisterTypes()
    {
        Container.RegisterType<IConfigurationHelper, ConfigurationHelper>();
        Container.RegisterType<IParameterMapper, SqlParameterMapper>();
        Container.RegisterType<IQuestionDataProvider, SqlQuestionDataProvider>();
        Container.RegisterType<ISettingsDataProvider, SqlSettingsDataProvider>();
        Container.RegisterType<ISettingsHelper, SettingsHelper>();
        Container.RegisterType<ISiteMapWrapper, SiteMapWrapper>();
        Container.RegisterType<IEmailProvider, SmtpEmailProvider>();
        Container.RegisterType<IHttpContextHelper, HttpContextHelper>();
        Container.RegisterType<IResourceHelper, ResourceHelper>();
        Container.RegisterType<ICacheHelper, HttpCacheHelper>();
        Container.RegisterType<IPageService, PageService>();
        Container.RegisterType<IUrlEntryBuilder, UrlEntryBuilder>();
        Container.RegisterType<IUrlEntryDataAccess, SqlUrlEntryDataAccess>();
        Container.RegisterType<IUrlEntryDataProvider, SqlUrlEntryDataProvider>();
        Container.RegisterType<IPageBuilder, PageBuilder>();
        Container.RegisterType<IPageDataAccess, SqlPageDataAccess>();
        Container.RegisterType<IPageDataProvider, SqlPageDataProvider>();
        Container.RegisterType<IIngredientDataProvider, LinqIngredientDataProvider>();
        Container.RegisterType<IModelConverter<UrlEntry>, UriModelConverter>();
        Container.RegisterType<IFormatter<string, string>, SeoFriendlyFormatter>();
        Container.RegisterType<IModelConverter<UrlEntry, SiteMapEntry>, SiteMapModelConverter>();
        Container.RegisterType<ISiteMapDataProvider, SiteMapDataProvider>();
    }
}
