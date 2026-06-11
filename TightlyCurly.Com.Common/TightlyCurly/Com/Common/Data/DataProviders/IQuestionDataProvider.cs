using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface IQuestionDataProvider
{
    Question GetQuestionById(int questionId);

    Question GetQuestionByQuestionAndAnswer(string question, string answer);

    IEnumerable<Question> GetQuestionsByCategory(int questionCategory);

    IEnumerable<Question> GetAllQuestions();

    Question SaveQuestion(int questionId, string questionValue, string questionAnswer, IEnumerable<QuestionCategory> questionCategories);

    void DeleteQuestionById(int questionId);

    void DeleteQuestionCategoryById(int questionCategoryId);

    IEnumerable<QuestionCategory> GetCategories(int? parentCategoryId);

    IEnumerable<QuestionCategory> GetCategoriesByCategoryIds(IEnumerable<int> questionCategoryIds);

    QuestionCategory SaveCategory(int questionCategoryId, int? parentCategoryId, string categoryName);

    IEnumerable<Question> GetQuestionsByCriteria(string criteria);
}
