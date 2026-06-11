using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace TightlyCurly.Com.Web.ContentServices;

[DataServiceKey("QuestionCategoryId")]
public class QuestionCategory
{
    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _QuestionCategoryId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _Category;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int? _ParentId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _EnteredDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _UpdatedDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private Collection<QuestionsQuestionCategory> _QuestionsQuestionCategories = new Collection<QuestionsQuestionCategory>();

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int QuestionCategoryId
    {
        get
        {
            return _QuestionCategoryId;
        }
        set
        {
            _QuestionCategoryId = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public string Category
    {
        get
        {
            return _Category;
        }
        set
        {
            _Category = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int? ParentId
    {
        get
        {
            return _ParentId;
        }
        set
        {
            _ParentId = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DateTime EnteredDate
    {
        get
        {
            return _EnteredDate;
        }
        set
        {
            _EnteredDate = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DateTime UpdatedDate
    {
        get
        {
            return _UpdatedDate;
        }
        set
        {
            _UpdatedDate = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public Collection<QuestionsQuestionCategory> QuestionsQuestionCategories
    {
        get
        {
            return _QuestionsQuestionCategories;
        }
        set
        {
            if (value != null)
            {
                _QuestionsQuestionCategories = value;
            }
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public static QuestionCategory CreateQuestionCategory(int questionCategoryId, DateTime enteredDate, DateTime updatedDate)
    {
        QuestionCategory questionCategory = new QuestionCategory();
        questionCategory.QuestionCategoryId = questionCategoryId;
        questionCategory.EnteredDate = enteredDate;
        questionCategory.UpdatedDate = updatedDate;
        return questionCategory;
    }
}
