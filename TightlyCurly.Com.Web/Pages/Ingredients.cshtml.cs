using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Web.Configuration;
using TightlyCurly.Com.Web.Controllers;
using TightlyCurly.Com.Web.Helpers;

namespace TightlyCurly.Com.Web.Pages;

/// <summary>
/// Replaces Ingredients.aspx + IngredientsControl + CategoryControl. Category link
/// clicks and the query string/route filters from the user control become GET
/// parameters; the data comes from the IngredientsController (called in-process),
/// which replaced the IngredientsPresenter.
/// </summary>
public class IngredientsModel : PageModel
{
    private readonly LayoutState _layoutState;

    private readonly IngredientsController _ingredients;

    private readonly IUrlEntryDataProvider _urlEntryDataProvider;

    public IngredientsModel(IOptions<IngredientsOptions> options, LayoutState layoutState, IngredientsController ingredients, IUrlEntryDataProvider urlEntryDataProvider)
    {
        Options = options.Value;
        _layoutState = layoutState;
        _ingredients = ingredients ?? throw new ArgumentNullException(nameof(ingredients));
        _urlEntryDataProvider = urlEntryDataProvider ?? throw new ArgumentNullException(nameof(urlEntryDataProvider));
    }

    public IngredientsOptions Options { get; }

    public List<Ingredient> Ingredients { get; private set; } = new();

    public bool ShowPreamble { get; private set; } = true;

    public IActionResult OnGet(string ingredientName, int? ingredientId, string startsWith)
    {
        ViewData["Title"] = Resources.TightlyCurly.Com.Web.ingredientsTitle;
        _layoutState.PageTitle = Resources.TightlyCurly.Com.Web.ingredientsDescription;

        List<Ingredient> results = QueryIngredients(ingredientName, ingredientId, startsWith);
        if (results != null)
        {
            Ingredients = results;
            SetTitle();
        }

        return Page();
    }

    private List<Ingredient> QueryIngredients(string ingredientName, int? ingredientId, string startsWith)
    {
        // Category link (formerly CategoryControl's LinkButton click).
        if (!string.IsNullOrEmpty(startsWith))
        {
            return _ingredients.GetIngredients(startsWith: startsWith).GetValue()?.ToList() ?? new();
        }

        // SEO route: /ingredients/{ingredientName} where the segment is a UrlEntry key.
        if (RouteData.Values.ContainsKey("ingredientName") && !string.IsNullOrEmpty(ingredientName))
        {
            string title = GetIngredientTitle(ingredientName);
            if (title == null)
            {
                return null;
            }
            return _ingredients.GetIngredients(title: title).GetValue()?.ToList() ?? new();
        }

        // Legacy query string filters (?ingredientId= / ?ingredientName=).
        if (ingredientId > 0)
        {
            Ingredient ingredient = _ingredients.GetIngredientById(ingredientId.Value).GetValue();
            return ingredient != null ? new List<Ingredient> { ingredient } : new();
        }
        string queryName = Request.Query[Common.Constants.IngredientsConstants.IngredientsNameQuery];
        if (!string.IsNullOrEmpty(queryName))
        {
            return _ingredients.GetIngredients(startsWith: queryName).GetValue()?.ToList() ?? new();
        }

        return null;
    }

    private string GetIngredientTitle(string ingredientName)
    {
        UrlEntry urlEntryByKey = _urlEntryDataProvider.GetUrlEntryByKey(ingredientName);
        if (urlEntryByKey == null)
        {
            return null;
        }
        return _ingredients.GetIngredientById(urlEntryByKey.ObjectId).GetValue()?.Title;
    }

    private void SetTitle()
    {
        if (!Ingredients.Any())
        {
            return;
        }
        string title;
        if (Ingredients.Count == 1)
        {
            title = Ingredients[0].Title;
            _layoutState.PageTitle = title;
        }
        else
        {
            string firstLetter = Ingredients[0].Title.Substring(0, 1);
            _layoutState.PageTitle = string.Format(
                Resources.TightlyCurly.Com.Web.ingredientsStartingWith,
                string.Format(Resources.TightlyCurly.Com.Web.formattedSpan, firstLetter));
            title = string.Format(Resources.TightlyCurly.Com.Web.ingredientsStartingWith, firstLetter);
        }
        ViewData["Title"] = title;
        ShowPreamble = string.IsNullOrEmpty(title);
    }

    public (string Text, string CssClass) GetRating(Ingredient ingredient)
    {
        return ingredient.IngredientRating switch
        {
            1 => (Common.Constants.IngredientsConstants.AvoidText, Options.AvoidStyle),
            2 => (Common.Constants.IngredientsConstants.CautionText, Options.CautionStyle),
            3 => (Common.Constants.IngredientsConstants.GoodText, Options.GoodStyle),
            _ => (Common.Constants.IngredientsConstants.AcceptableText, Options.AcceptableStyle)
        };
    }

    public string FormatRelatedLinks(Ingredient ingredient)
    {
        if (string.IsNullOrEmpty(ingredient.InternalLinks))
        {
            return string.Empty;
        }
        var builder = new System.Text.StringBuilder();
        foreach (string link in ingredient.InternalLinks.Split(Common.Constants.IngredientsConstants.Delimiter))
        {
            if (!string.IsNullOrEmpty(link))
            {
                builder.Append($"<a href=\"{Options.LinkUrl}?ingredientName={link}\">{link}</a> ");
            }
        }
        return builder.ToString();
    }

    public string FormatExternalLinks(Ingredient ingredient)
    {
        if (string.IsNullOrEmpty(ingredient.ExternalLinks))
        {
            return string.Empty;
        }
        var builder = new System.Text.StringBuilder();
        foreach (string link in ingredient.ExternalLinks.Split(Common.Constants.IngredientsConstants.Delimiter))
        {
            if (!string.IsNullOrEmpty(link))
            {
                string text = string.Empty;
                string href = link;
                if (href.StartsWith(Common.Constants.Anchor))
                {
                    text = href.Replace(Common.Constants.Anchor, string.Empty);
                    href = $"{Options.LinkUrl}{href}";
                }
                builder.Append($"<a href=\"{href}\">{(text == string.Empty ? href : text)}</a> ");
            }
        }
        return builder.ToString();
    }
}
