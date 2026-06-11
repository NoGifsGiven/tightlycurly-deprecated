using System;
using System.CodeDom.Compiler;
using System.Data.Services.Client;

namespace TightlyCurly.Com.Web.ContentServices;

public class DataEntitiesDataContext : DataServiceContext
{
    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DataServiceQuery<QuestionsQuestionCategory> _QuestionsQuestionCategories;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DataServiceQuery<QuestionCategory> _QuestionCategories;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DataServiceQuery<Question> _Questions;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DataServiceQuery<QuestionsQuestionCategory> QuestionsQuestionCategories
    {
        get
        {
            if (_QuestionsQuestionCategories == null)
            {
                _QuestionsQuestionCategories = ((DataServiceContext)this).CreateQuery<QuestionsQuestionCategory>("QuestionsQuestionCategories");
            }
            return _QuestionsQuestionCategories;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DataServiceQuery<QuestionCategory> QuestionCategories
    {
        get
        {
            if (_QuestionCategories == null)
            {
                _QuestionCategories = ((DataServiceContext)this).CreateQuery<QuestionCategory>("QuestionCategories");
            }
            return _QuestionCategories;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DataServiceQuery<Question> Questions
    {
        get
        {
            if (_Questions == null)
            {
                _Questions = ((DataServiceContext)this).CreateQuery<Question>("Questions");
            }
            return _Questions;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DataEntitiesDataContext(Uri serviceRoot)
        : base(serviceRoot)
    {
        ((DataServiceContext)this).ResolveName = ResolveNameFromType;
        ((DataServiceContext)this).ResolveType = ResolveTypeFromName;
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    protected Type ResolveTypeFromName(string typeName)
    {
        if (typeName.StartsWith("Alligoshee.Hair.Business", StringComparison.Ordinal))
        {
            return ((object)this).GetType().Assembly.GetType("TightlyCurly.Com.Web.ContentServices" + typeName.Substring(24), throwOnError: false);
        }
        return null;
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    protected string ResolveNameFromType(Type clientType)
    {
        if (clientType.Namespace.Equals("TightlyCurly.Com.Web.ContentServices", StringComparison.Ordinal))
        {
            return "Alligoshee.Hair.Business." + clientType.Name;
        }
        return null;
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public void AddToQuestionsQuestionCategories(QuestionsQuestionCategory questionsQuestionCategory)
    {
        ((DataServiceContext)this).AddObject("QuestionsQuestionCategories", (object)questionsQuestionCategory);
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public void AddToQuestionCategories(QuestionCategory questionCategory)
    {
        ((DataServiceContext)this).AddObject("QuestionCategories", (object)questionCategory);
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public void AddToQuestions(Question question)
    {
        ((DataServiceContext)this).AddObject("Questions", (object)question);
    }
}
