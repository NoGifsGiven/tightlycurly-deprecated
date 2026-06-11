using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Web.Controllers;
using TightlyCurly.Com.Web.Helpers;

namespace TightlyCurly.Com.Web.Pages;

/// <summary>
/// Replaces AnswerBank.aspx. Postback event handlers become GET parameters
/// (masterCategoryId, subcategoryId, questionId, search). The data comes from the
/// AnswerBankController (called in-process), which replaced the AnswerBankPresenter.
/// </summary>
public class AnswerBankModel : PageModel
{
    private readonly AnswerBankController _answerBank;

    private readonly IUrlEntryDataProvider _urlEntryDataProvider;

    public AnswerBankModel(AnswerBankController answerBank, IUrlEntryDataProvider urlEntryDataProvider)
    {
        _answerBank = answerBank ?? throw new ArgumentNullException(nameof(answerBank));
        _urlEntryDataProvider = urlEntryDataProvider ?? throw new ArgumentNullException(nameof(urlEntryDataProvider));
    }

    public IEnumerable<QuestionCategory> MasterCategories { get; set; } = Enumerable.Empty<QuestionCategory>();

    public IEnumerable<QuestionCategory> Subcategories { get; set; } = Enumerable.Empty<QuestionCategory>();

    public IEnumerable<Question> SelectedQuestions { get; set; } = Enumerable.Empty<Question>();

    public IEnumerable<Question> AllQuestions { get; set; } = Enumerable.Empty<Question>();

    public int? SelectedMasterCategoryId { get; private set; }

    public int? SelectedSubcategoryId { get; private set; }

    public string SearchText { get; private set; }

    public IActionResult OnGet(string questionName, int? questionId, int? masterCategoryId, int? subcategoryId, string search)
    {
        ViewData["Title"] = "Answer Bank";

        MasterCategories = _answerBank.GetMasterCategories().GetValue() ?? Enumerable.Empty<QuestionCategory>();
        AllQuestions = _answerBank.GetAllQuestions().GetValue() ?? Enumerable.Empty<Question>();

        if (masterCategoryId > 0)
        {
            SelectedMasterCategoryId = masterCategoryId;
            Subcategories = _answerBank.GetSubcategoriesByParentId(masterCategoryId.Value).GetValue() ?? Enumerable.Empty<QuestionCategory>();
        }

        if (!string.IsNullOrEmpty(search))
        {
            SearchText = search;
            SelectedQuestions = _answerBank.GetQuestionsByCriteria(search).GetValue() ?? Enumerable.Empty<Question>();
        }
        else if (subcategoryId > 0)
        {
            SelectedSubcategoryId = subcategoryId;
            SelectedQuestions = _answerBank.GetQuestionsBySubcategoryId(subcategoryId.Value).GetValue() ?? Enumerable.Empty<Question>();
        }
        else
        {
            int resolvedQuestionId = ResolveQuestionId(questionName, questionId);
            if (resolvedQuestionId > 0)
            {
                Question question = _answerBank.GetQuestionById(resolvedQuestionId).GetValue();
                if (question != null)
                {
                    SelectedQuestions = new[] { question };
                }
            }
        }

        return Page();
    }

    private int ResolveQuestionId(string questionName, int? questionId)
    {
        if (questionId > 0)
        {
            return questionId.Value;
        }
        if (!string.IsNullOrEmpty(questionName))
        {
            // SEO route: the question name is a UrlEntry key pointing at the question.
            UrlEntry urlEntryByKey = _urlEntryDataProvider.GetUrlEntryByKey(questionName);
            if (urlEntryByKey == null)
            {
                return 0;
            }
            return _answerBank.GetQuestionById(urlEntryByKey.ObjectId).GetValue()?.QuestionId ?? 0;
        }
        return 0;
    }
}
