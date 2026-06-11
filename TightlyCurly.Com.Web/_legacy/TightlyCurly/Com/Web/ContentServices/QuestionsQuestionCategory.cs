using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace TightlyCurly.Com.Web.ContentServices;

[DataServiceKey("QuestionsQuestionCategoriesId")]
public class QuestionsQuestionCategory
{
    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _QuestionsQuestionCategoriesId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _QuestionId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _QuestionCategoryId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _EnteredDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _UpdatedDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private QuestionCategory _QuestionCategory;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private Question _Question;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int QuestionsQuestionCategoriesId
    {
        get
        {
            return _QuestionsQuestionCategoriesId;
        }
        set
        {
            _QuestionsQuestionCategoriesId = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int QuestionId
    {
        get
        {
            return _QuestionId;
        }
        set
        {
            _QuestionId = value;
        }
    }

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
    public QuestionCategory QuestionCategory
    {
        get
        {
            return _QuestionCategory;
        }
        set
        {
            _QuestionCategory = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public Question Question
    {
        get
        {
            return _Question;
        }
        set
        {
            _Question = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public static QuestionsQuestionCategory CreateQuestionsQuestionCategory(int questionsQuestionCategoriesId, int questionId, int questionCategoryId, DateTime enteredDate, DateTime updatedDate)
    {
        QuestionsQuestionCategory questionsQuestionCategory = new QuestionsQuestionCategory();
        questionsQuestionCategory.QuestionsQuestionCategoriesId = questionsQuestionCategoriesId;
        questionsQuestionCategory.QuestionId = questionId;
        questionsQuestionCategory.QuestionCategoryId = questionCategoryId;
        questionsQuestionCategory.EnteredDate = enteredDate;
        questionsQuestionCategory.UpdatedDate = updatedDate;
        return questionsQuestionCategory;
    }
}
