using System;
using System.Linq;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Presenters;

public class AnswerBankPresenter : IPresenter
{
    protected readonly IAnswerBankView _view;

    protected readonly IQuestionDataProvider _questionDataProvider;

    protected readonly IModelConverter<UrlEntry> _modelConverter;

    public AnswerBankPresenter(IAnswerBankView view, IQuestionDataProvider questionDataProvider, IModelConverter<UrlEntry> modelConverter)
    {
        if (view == null)
        {
            throw new ArgumentNullException("view");
        }
        if (questionDataProvider == null)
        {
            throw new ArgumentNullException("questionDataProvider");
        }
        if (modelConverter == null)
        {
            throw new ArgumentNullException("modelConverter");
        }
        _view = view;
        _questionDataProvider = questionDataProvider;
        _modelConverter = modelConverter;
    }

    public void GetMasterCategories()
    {
        _view.MasterCategories = from qc in _questionDataProvider.GetCategories(null)
            orderby qc.Category
            select qc;
    }

    public void GetAllQuestions()
    {
        _view.AllQuestions = _questionDataProvider.GetAllQuestions();
    }

    public void GetQuestionById(int questionId)
    {
        if (questionId <= 0)
        {
            throw new ArgumentException("questionId cannot be less than or equal to zero.", "questionId");
        }
        _view.SelectedQuestions = new Question[1] { _questionDataProvider.GetQuestionById(questionId) };
    }

    public void GetSubcategoriesByParentId(int parentCategoryId)
    {
        if (parentCategoryId <= 0)
        {
            throw new ArgumentException("parentCategoryId cannot be less than or equal to zero.", "parentCategoryId");
        }
        _view.Subcategories = from qc in _questionDataProvider.GetCategories(parentCategoryId)
            orderby qc.Category
            select qc;
    }

    public void GetQuestionsBySubcategoryId(int subcategoryId)
    {
        if (subcategoryId <= 0)
        {
            throw new ArgumentException("subcategoryId cannot be less than or equal to zero.", "subcategoryId");
        }
        _view.SelectedQuestions = from q in _questionDataProvider.GetQuestionsByCategory(subcategoryId)
            orderby q.Value
            select q;
    }

    public void GetQuestionsByCriteria(string criteria)
    {
        if (string.IsNullOrEmpty(criteria))
        {
            throw new ArgumentNullException("criteria");
        }
        _view.SelectedQuestions = from q in _questionDataProvider.GetQuestionsByCriteria(criteria)
            orderby q.Value
            select q;
    }
}
