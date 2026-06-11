using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public class SqlQuestionDataProvider : DataAdapterBase, IQuestionDataProvider
{
    private readonly IConfigurationHelper _configuration;

    public SqlQuestionDataProvider(IConfigurationHelper configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _connectionString = _configuration.DefaultConnectionString;
    }

    // Synchronous IQuestionDataProvider implementation (delegates to the async methods).

    public Question GetQuestionById(int questionId)
    {
        return GetQuestionByIdAsync(questionId).GetAwaiter().GetResult();
    }

    public Question GetQuestionByQuestionAndAnswer(string question, string answer)
    {
        return GetQuestionByQuestionAndAnswerAsync(question, answer).GetAwaiter().GetResult();
    }

    public IEnumerable<Question> GetQuestionsByCategory(int questionCategory)
    {
        return GetQuestionsByCategoryAsync(questionCategory).GetAwaiter().GetResult();
    }

    public IEnumerable<Question> GetAllQuestions()
    {
        return GetAllQuestionsAsync().GetAwaiter().GetResult();
    }

    public Question SaveQuestion(int questionId, string questionValue, string questionAnswer, IEnumerable<QuestionCategory> questionCategories)
    {
        return SaveQuestionAsync(questionId, questionValue, questionAnswer, questionCategories).GetAwaiter().GetResult();
    }

    public void DeleteQuestionById(int questionId)
    {
        DeleteQuestionByIdAsync(questionId).GetAwaiter().GetResult();
    }

    public void DeleteQuestionCategoryById(int questionCategoryId)
    {
        DeleteQuestionCategoryByIdAsync(questionCategoryId).GetAwaiter().GetResult();
    }

    public IEnumerable<QuestionCategory> GetCategories(int? parentCategoryId)
    {
        return GetCategoriesAsync(parentCategoryId).GetAwaiter().GetResult();
    }

    public IEnumerable<QuestionCategory> GetCategoriesByCategoryIds(IEnumerable<int> questionCategoryIds)
    {
        return GetCategoriesByCategoryIdsAsync(questionCategoryIds).GetAwaiter().GetResult();
    }

    public QuestionCategory SaveCategory(int questionCategoryId, int? parentCategoryId, string categoryName)
    {
        return SaveCategoryAsync(questionCategoryId, parentCategoryId, categoryName).GetAwaiter().GetResult();
    }

    public IEnumerable<Question> GetQuestionsByCriteria(string criteria)
    {
        return GetQuestionsByCriteriaAsync(criteria).GetAwaiter().GetResult();
    }

    public async Task<Question> GetQuestionByIdAsync(int questionId, CancellationToken cancellationToken = default)
    {
        if (questionId <= 0)
        {
            throw new ArgumentException("questionId cannot be zero or negative.", "questionId");
        }
        
        var question = new Question();
        var categories = new List<QuestionCategory>();
        
        using (DbDataReader dataReader = await base.DataAccess.GetQuestionByIdAsync(questionId, cancellationToken))
        {
            while (dataReader.Read())
            {
                question = LoadQuestion(dataReader);
                
                int questionCategoryId = Convert.ToInt32(dataReader["QuestionCategoryId"]);
                if (!categories.Any(qc => qc.QuestionCategoryId == questionCategoryId))
                {
                    categories.Add(LoadQuestionCategory(dataReader));
                }
            }
        }
        
        question.QuestionCategories = categories;
        return question;
    }

    public async Task<Question> GetQuestionByQuestionAndAnswerAsync(string question, string answer, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(question))
        {
            throw new ArgumentNullException(nameof(question));
        }
        
        if (string.IsNullOrEmpty(answer))
        {
            throw new ArgumentNullException(nameof(answer));
        }
        
        var question2 = new Question();
        var categories = new List<QuestionCategory>();
        
        using (DbDataReader dataReader = await base.DataAccess.GetQuestionByQuestionAndAnswerAsync(question, answer, cancellationToken))
        {
            while (dataReader.Read())
            {
                question2 = LoadQuestion(dataReader);
                
                int questionCategoryId = Convert.ToInt32(dataReader["QuestionCategoryId"]);
                if (!categories.Any(qc => qc.QuestionCategoryId == questionCategoryId))
                {
                    categories.Add(LoadQuestionCategory(dataReader));
                }
            }
        }
        
        question2.QuestionCategories = categories;
        return question2;
    }

    public async Task<IEnumerable<Question>> GetQuestionsByCategoryAsync(int questionCategoryId, CancellationToken cancellationToken = default)
    {
        if (questionCategoryId <= 0)
        {
            throw new ArgumentException("questionCategoryId must be greater than or equal to zero.", "questionCategoryId");
        }
        
        var dictionary = new Dictionary<int, Question>();
        using (DbDataReader dataReader = await base.DataAccess.GetQuestionsByCategoryAsync(questionCategoryId, cancellationToken))
        {
            while (dataReader.Read())
            {
                int key = Convert.ToInt32(dataReader["QuestionId"]);
                int categoryId = Convert.ToInt32(dataReader["QuestionCategoryId"]);
                
                Question question;
                if (dictionary.ContainsKey(key))
                {
                    question = dictionary[key];
                }
                else
                {
                    question = LoadQuestion(dataReader);
                    dictionary.Add(question.QuestionId, question);
                }
                
                var existingCategory = question.QuestionCategories.FirstOrDefault(qc => qc.QuestionCategoryId == categoryId);
                if (existingCategory == null)
                {
                    var categoriesList = question.QuestionCategories.ToList();
                    categoriesList.Add(LoadQuestionCategory(dataReader));
                    question.QuestionCategories = categoriesList;
                }
                
                dictionary[key] = question;
            }
        }
        
        return dictionary.Values.Select(q => q);
    }

    public async Task<IEnumerable<Question>> GetAllQuestionsAsync(CancellationToken cancellationToken = default)
    {
        var dictionary = new Dictionary<int, Question>();
        using (DbDataReader dataReader = await base.DataAccess.GetAllQuestionsAsync(cancellationToken))
        {
            while (dataReader.Read())
            {
                int key = Convert.ToInt32(dataReader["QuestionId"]);
                int categoryId = Convert.ToInt32(dataReader["QuestionCategoryId"]);
                
                Question question;
                if (dictionary.ContainsKey(key))
                {
                    question = dictionary[key];
                }
                else
                {
                    question = LoadQuestion(dataReader);
                    dictionary.Add(question.QuestionId, question);
                }
                
                var existingCategory = question.QuestionCategories.FirstOrDefault(qc => qc.QuestionCategoryId == categoryId);
                if (existingCategory == null)
                {
                    var categoriesList = question.QuestionCategories.ToList();
                    categoriesList.Add(LoadQuestionCategory(dataReader));
                    question.QuestionCategories = categoriesList;
                }
                
                dictionary[key] = question;
            }
        }
        
        return dictionary.Values.Select(q => q);
    }

    public async Task<Question> SaveQuestionAsync(int questionId, string questionValue, string questionAnswer, IEnumerable<QuestionCategory> questionCategories, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(questionValue))
        {
            throw new ArgumentNullException(nameof(questionValue));
        }
        
        if (string.IsNullOrEmpty(questionAnswer))
        {
            throw new ArgumentNullException(nameof(questionAnswer));
        }
        
        if (questionCategories == null || !questionCategories.Any())
        {
            throw new ArgumentNullException(nameof(questionCategories));
        }
        
        if (questionId < 0)
        {
            throw new ArgumentException("questionId cannot be negative.", "questionId");
        }
        
        var question = new Question();
        var categories = new List<QuestionCategory>();
        string questionCategoryIds = string.Join(",", questionCategories.Select(q => q.QuestionCategoryId.ToString()).ToArray());
        
        using (DbDataReader dataReader = await base.DataAccess.SaveQuestionAsync(questionId, questionValue, questionAnswer, Constants.DefaultLocale.LocaleId, questionCategoryIds, cancellationToken))
        {
            while (dataReader.Read())
            {
                question = LoadQuestion(dataReader);
                
                int questionCategoryId = Convert.ToInt32(dataReader["QuestionCategoryId"]);
                if (!categories.Any(qc => qc.QuestionCategoryId == questionCategoryId))
                {
                    categories.Add(LoadQuestionCategory(dataReader));
                }
            }
        }
        
        question.QuestionCategories = categories;
        return question;
    }

    public async Task DeleteQuestionByIdAsync(int questionId, CancellationToken cancellationToken = default)
    {
        if (questionId <= 0)
        {
            throw new ArgumentException("questionId cannot be less than or equal to zero.", "questionId");
        }
        
        await base.DataAccess.DeleteQuestionByIdAsync(questionId, cancellationToken);
    }

    public async Task DeleteQuestionCategoryByIdAsync(int questionCategoryId, CancellationToken cancellationToken = default)
    {
        if (questionCategoryId <= 0)
        {
            throw new ArgumentException("questionCategoryId cannot be less than or equal to zero.", "questionCategoryId");
        }
        
        await base.DataAccess.DeleteQuestionCategoryByIdAsync(questionCategoryId, cancellationToken);
    }

    public async Task<IEnumerable<QuestionCategory>> GetCategoriesAsync(int? parentCategoryId, CancellationToken cancellationToken = default)
    {
        if (parentCategoryId.HasValue && parentCategoryId.Value <= 0)
        {
            throw new ArgumentException("parentCategoryId cannot be less than or equal to zero.", "parentCategoryId");
        }
        
        var categories = new List<QuestionCategory>();
        using (DbDataReader dataReader = await base.DataAccess.GetQuestionCategoriesAsync(parentCategoryId, cancellationToken))
        {
            while (dataReader.Read())
            {
                categories.Add(LoadQuestionCategory(dataReader));
            }
        }
        
        return categories;
    }

    public async Task<IEnumerable<QuestionCategory>> GetCategoriesByCategoryIdsAsync(IEnumerable<int> questionCategoryIds, CancellationToken cancellationToken = default)
    {
        if (questionCategoryIds == null || !questionCategoryIds.Any())
        {
            throw new ArgumentNullException(nameof(questionCategoryIds));
        }
        
        var categories = new List<QuestionCategory>();
        using (DbDataReader dataReader = await base.DataAccess.GetQuestionCategoriesByCategoryIdsAsync(string.Join(",", questionCategoryIds.Select(q => q.ToString()).ToArray()), cancellationToken))
        {
            while (dataReader.Read())
            {
                categories.Add(LoadQuestionCategory(dataReader));
            }
        }
        
        return categories;
    }

    public async Task<QuestionCategory> SaveCategoryAsync(int questionCategoryId, int? parentCategoryId, string categoryName, CancellationToken cancellationToken = default)
    {
        if (questionCategoryId < 0)
        {
            throw new ArgumentException("questionCategoryId cannot be less than zero.", "questionCategoryId");
        }
        
        if (parentCategoryId <= 0)
        {
            throw new ArgumentException("parentCategoryId cannot be less than or equal to zero.", "parentCategoryId");
        }
        
        if (string.IsNullOrEmpty(categoryName))
        {
            throw new ArgumentNullException(nameof(categoryName));
        }
        
        var result = new QuestionCategory();
        using (DbDataReader dataReader = await base.DataAccess.SaveQuestionCategoryAsync(questionCategoryId, parentCategoryId, categoryName, cancellationToken))
        {
            while (dataReader.Read())
            {
                result = LoadQuestionCategory(dataReader);
            }
        }
        
        return result;
    }

    public async Task<IEnumerable<Question>> GetQuestionsByCriteriaAsync(string criteria, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(criteria))
        {
            throw new ArgumentNullException(nameof(criteria));
        }
        
        var dictionary = new Dictionary<int, Question>();
        using (DbDataReader dataReader = await base.DataAccess.GetQuestionsByCriteriaAsync(criteria, cancellationToken))
        {
            while (dataReader.Read())
            {
                int key = Convert.ToInt32(dataReader["QuestionId"]);
                int categoryId = Convert.ToInt32(dataReader["QuestionCategoryId"]);
                
                Question question;
                if (dictionary.ContainsKey(key))
                {
                    question = dictionary[key];
                }
                else
                {
                    question = LoadQuestion(dataReader);
                    dictionary.Add(question.QuestionId, question);
                }
                
                var existingCategory = question.QuestionCategories.FirstOrDefault(qc => qc.QuestionCategoryId == categoryId);
                if (existingCategory == null)
                {
                    var categoriesList = question.QuestionCategories.ToList();
                    categoriesList.Add(LoadQuestionCategory(dataReader));
                    question.QuestionCategories = categoriesList;
                }
                
                dictionary[key] = question;
            }
        }
        
        return dictionary.Values.Select(q => q);
    }

    internal Question LoadQuestion(DbDataReader reader)
    {
        var question = new Question();
        
        if (reader != null)
        {
            question.QuestionId = Convert.ToInt32(reader["QuestionId"]);
            question.Value = reader["Question"]?.ToString() ?? string.Empty;
            question.Answer = reader["Answer"]?.ToString() ?? string.Empty;
            
            // Safe date parsing with fallback to current time
            var enteredDateValue = reader["EnteredDate"]?.ToString();
            if (!string.IsNullOrEmpty(enteredDateValue) && DateTimeOffset.TryParse(enteredDateValue, out var enteredDate))
            {
                question.EnteredDate = enteredDate;
            }
            
            var updatedDateValue = reader["UpdatedDate"]?.ToString();
            if (!string.IsNullOrEmpty(updatedDateValue) && DateTimeOffset.TryParse(updatedDateValue, out var updatedDate))
            {
                question.UpdatedDate = updatedDate;
            }
            
            // Safe locale object creation with fallbacks
            if (reader["LocaleId"] != DBNull.Value && reader["LCID"] != DBNull.Value)
            {
                question.Locale = new Locale
                {
                    LocaleId = Convert.ToInt32(reader["LocaleId"]),
                    LCID = Convert.ToInt32(reader["LCID"]),
                    LocaleName = reader["LocaleName"]?.ToString() ?? string.Empty
                };
            }
        }
        
        return question;
    }

    internal QuestionCategory LoadQuestionCategory(DbDataReader reader)
    {
        var questionCategory = new QuestionCategory();
        
        if (reader != null)
        {
            questionCategory.QuestionCategoryId = Convert.ToInt32(reader["QuestionCategoryId"]);
            questionCategory.Category = reader["Category"]?.ToString() ?? string.Empty;
            
            // Safe date parsing with fallback to current time
            var enteredDateValue = reader["EnteredDate"]?.ToString();
            if (!string.IsNullOrEmpty(enteredDateValue) && DateTimeOffset.TryParse(enteredDateValue, out var enteredDate))
            {
                questionCategory.EnteredDate = enteredDate;
            }
            
            // Safe nullable int parsing with fallback to null
            var parentIdValue = reader["ParentId"];
            if (parentIdValue != DBNull.Value)
            {
                questionCategory.ParentId = Convert.ToInt32(parentIdValue);
            }
            else
            {
                questionCategory.ParentId = null;
            }
            
            var updatedDateValue = reader["UpdatedDate"]?.ToString();
            if (!string.IsNullOrEmpty(updatedDateValue) && DateTimeOffset.TryParse(updatedDateValue, out var updatedDate))
            {
                questionCategory.UpdatedDate = updatedDate;
            }
        }
        
        return questionCategory;
    }
}