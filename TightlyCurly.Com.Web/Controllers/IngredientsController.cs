using Microsoft.AspNetCore.Mvc;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Web.Utilities;

namespace TightlyCurly.Com.Web.Controllers;

/// <summary>
/// Replaces IngredientsPresenter (and the original WCF Data Services ProductService
/// feed). The presenter's Func&lt;Ingredient, bool&gt; filter becomes query
/// parameters; results keep the IngredientComparer title ordering.
/// </summary>
[ApiController]
[Route("api/ingredients")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientDataProvider _ingredientDataProvider;

    public IngredientsController(IIngredientDataProvider ingredientDataProvider)
    {
        _ingredientDataProvider = ingredientDataProvider ?? throw new ArgumentNullException(nameof(ingredientDataProvider));
    }

    // Replaces GetIngredients(filter): no parameters returns everything; startsWith
    // is a case-insensitive title prefix (formerly the CategoryControl letter links);
    // title is an exact match (formerly the SEO-route filter).
    [HttpGet]
    public ActionResult<IEnumerable<Ingredient>> GetIngredients([FromQuery] string startsWith = null, [FromQuery] string title = null)
    {
        var ingredients = _ingredientDataProvider.GetAllIngredients() ?? Enumerable.Empty<Ingredient>();
        if (!string.IsNullOrEmpty(title))
        {
            ingredients = ingredients.Where(i => i.Title == title);
        }
        else if (!string.IsNullOrEmpty(startsWith))
        {
            string prefix = TextEncoder.SafeEncode(startsWith);
            ingredients = ingredients.Where(i => i.Title != null && i.Title.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }
        return Ok(ingredients.OrderBy(i => i.Title, new IngredientComparer()));
    }

    [HttpGet("{ingredientId:int}")]
    public ActionResult<Ingredient> GetIngredientById(int ingredientId)
    {
        if (ingredientId <= 0)
        {
            return BadRequest("ingredientId cannot be less than or equal to zero.");
        }
        var ingredient = _ingredientDataProvider.GetIngredientById(ingredientId);
        if (ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient);
    }
}
