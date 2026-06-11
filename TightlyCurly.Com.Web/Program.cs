using Microsoft.AspNetCore.Mvc.RazorPages;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Web;
using TightlyCurly.Com.Web.Configuration;
using TightlyCurly.Com.Web.Helpers;
using TightlyCurly.Com.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Replaces Global.asax Application_Start and the legacy static Castle Windsor
// container: the whole type graph lives in the standard DI container.
builder.Services.AddTightlyCurlyServices(builder.Configuration);

builder.Services.Configure<IngredientsOptions>(builder.Configuration.GetSection(IngredientsOptions.SectionName));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<LayoutState>();

// AddControllersAsServices lets the Razor Pages constructor-inject the API
// controllers and call them in-process.
builder.Services.AddControllers().AddControllersAsServices();

builder.Services
    .AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add(new MasterViewPageFilter()));

// Replaces Bootstrapper.RegisterRoutes: dynamic routes from the UrlRoutes table,
// resolved through DI when the Razor Pages options are first built.
builder.Services.AddOptions<RazorPagesOptions>().Configure<IUrlEntryDataProvider, ILoggerFactory>(
    (options, urlEntryDataProvider, loggerFactory) => Bootstrapper.AddDynamicPageRoutes(
        options.Conventions, urlEntryDataProvider, loggerFactory.CreateLogger(nameof(Bootstrapper))));

var app = builder.Build();

// The legacy static service locator (still used by the User model's static methods)
// now resolves from the application container.
Container.Initialize(app.Services);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error/500");
}
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapControllers();

// Replaces the WCF AutoCompleteService used by the AjaxControlToolkit extenders.
app.MapGet("/api/autocomplete/ingredients", (string prefixText, int? count, AutoCompleteService autoComplete) =>
    Results.Json(autoComplete.GetIngredientsNameList(prefixText ?? string.Empty, count ?? 0)));
app.MapGet("/api/autocomplete/questions", (string prefixText, int? count, AutoCompleteService autoComplete) =>
    Results.Json(autoComplete.GetQuestionsList(prefixText ?? string.Empty, count ?? 0)));

app.Run();
