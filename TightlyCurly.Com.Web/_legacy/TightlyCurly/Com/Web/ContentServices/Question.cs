using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace TightlyCurly.Com.Web.ContentServices;

[DataServiceKey("QuestionId")]
public class Question
{
    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _QuestionId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _Question1;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _Answer;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _LocaleId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _EnteredDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _UpdatedDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private Collection<QuestionsQuestionCategory> _QuestionsQuestionCategories = new Collection<QuestionsQuestionCategory>();

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
    public string Question1
    {
        get
        {
            return _Question1;
        }
        set
        {
            _Question1 = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public string Answer
    {
        get
        {
            return _Answer;
        }
        set
        {
            _Answer = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int LocaleId
    {
        get
        {
            return _LocaleId;
        }
        set
        {
            _LocaleId = value;
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
    public static Question CreateQuestion(int questionId, int localeId, DateTime enteredDate, DateTime updatedDate)
    {
        Question question = new Question();
        question.QuestionId = questionId;
        question.LocaleId = localeId;
        question.EnteredDate = enteredDate;
        question.UpdatedDate = updatedDate;
        return question;
    }
}
