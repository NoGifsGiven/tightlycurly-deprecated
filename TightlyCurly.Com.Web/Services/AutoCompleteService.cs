using TightlyCurly.Com.Common.Data.DataProviders;

namespace TightlyCurly.Com.Web.Services;

/// <summary>
/// Formerly a WCF (System.ServiceModel) service consumed by the AjaxControlToolkit
/// AutoCompleteExtender; now exposed via minimal API endpoints (see Program.cs).
/// </summary>
public class AutoCompleteService
{
    private readonly IQuestionDataProvider _questionDataProvider;

    private readonly IIngredientDataProvider _ingredientDataProvider;

    public AutoCompleteService(IQuestionDataProvider questionDataProvider, IIngredientDataProvider ingredientDataProvider)
    {
        _questionDataProvider = questionDataProvider ?? throw new ArgumentNullException(nameof(questionDataProvider));
        _ingredientDataProvider = ingredientDataProvider ?? throw new ArgumentNullException(nameof(ingredientDataProvider));
    }

    public string[] GetIngredientsNameList(string prefixText, int count)
    {
        var titles = _ingredientDataProvider.GetAllIngredients()
            .Select(i => i.Title)
            .Where(t => !string.IsNullOrEmpty(t) && t.ToLower().Contains((prefixText ?? string.Empty).ToLower()));
        if (count > 0)
        {
            titles = titles.Take(count);
        }
        return titles.ToArray();
    }

    public string[] GetQuestionsList(string prefixText, int count)
    {
        var questions = _questionDataProvider.GetQuestionsByCriteria(prefixText)
            .Select(q => q.Value);
        if (count > 0)
        {
            questions = questions.Take(count);
        }
        return questions.ToArray();
    }
}
