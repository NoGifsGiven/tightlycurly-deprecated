using Microsoft.AspNetCore.Mvc;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Controllers;

/// <summary>
/// Replaces AnswerBankPresenter. Instead of pushing results into an IAnswerBankView,
/// each presenter method becomes a GET endpoint returning the data directly; argument
/// guards become 400 responses. The unused IModelConverter&lt;UrlEntry&gt; dependency
/// was dropped.
/// </summary>
[ApiController]
[Route("api/answerbank")]
public class AnswerBankController : ControllerBase
{
    private readonly IQuestionDataProvider _questionDataProvider;

    public AnswerBankController(IQuestionDataProvider questionDataProvider)
    {
        _questionDataProvider = questionDataProvider ?? throw new ArgumentNullException(nameof(questionDataProvider));
    }

    // Replaces GetMasterCategories (view.MasterCategories).
    [HttpGet("categories")]
    public ActionResult<IEnumerable<QuestionCategory>> GetMasterCategories()
    {
        return Ok(_questionDataProvider.GetCategories(null).OrderBy(qc => qc.Category));
    }

    // Replaces GetSubcategoriesByParentId (view.Subcategories).
    [HttpGet("categories/{parentCategoryId:int}/subcategories")]
    public ActionResult<IEnumerable<QuestionCategory>> GetSubcategoriesByParentId(int parentCategoryId)
    {
        if (parentCategoryId <= 0)
        {
            return BadRequest("parentCategoryId cannot be less than or equal to zero.");
        }
        return Ok(_questionDataProvider.GetCategories(parentCategoryId).OrderBy(qc => qc.Category));
    }

    // Replaces GetAllQuestions (view.AllQuestions).
    [HttpGet("questions")]
    public ActionResult<IEnumerable<Question>> GetAllQuestions()
    {
        return Ok(_questionDataProvider.GetAllQuestions());
    }

    // Replaces GetQuestionById (view.SelectedQuestions, a single-element array).
    [HttpGet("questions/{questionId:int}")]
    public ActionResult<Question> GetQuestionById(int questionId)
    {
        if (questionId <= 0)
        {
            return BadRequest("questionId cannot be less than or equal to zero.");
        }
        var question = _questionDataProvider.GetQuestionById(questionId);
        if (question == null)
        {
            return NotFound();
        }
        return Ok(question);
    }

    // Replaces GetQuestionsBySubcategoryId (view.SelectedQuestions).
    [HttpGet("categories/{subcategoryId:int}/questions")]
    public ActionResult<IEnumerable<Question>> GetQuestionsBySubcategoryId(int subcategoryId)
    {
        if (subcategoryId <= 0)
        {
            return BadRequest("subcategoryId cannot be less than or equal to zero.");
        }
        return Ok(_questionDataProvider.GetQuestionsByCategory(subcategoryId).OrderBy(q => q.Value));
    }

    // Replaces GetQuestionsByCriteria (view.SelectedQuestions).
    [HttpGet("questions/search")]
    public ActionResult<IEnumerable<Question>> GetQuestionsByCriteria([FromQuery] string criteria)
    {
        if (string.IsNullOrEmpty(criteria))
        {
            return BadRequest("criteria is required.");
        }
        return Ok(_questionDataProvider.GetQuestionsByCriteria(criteria).OrderBy(q => q.Value));
    }
}
