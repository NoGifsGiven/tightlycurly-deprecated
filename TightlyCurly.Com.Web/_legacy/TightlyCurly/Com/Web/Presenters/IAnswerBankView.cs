using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Presenters;

public interface IAnswerBankView
{
    IEnumerable<QuestionCategory> MasterCategories { get; set; }

    IEnumerable<QuestionCategory> Subcategories { get; set; }

    IEnumerable<Question> SelectedQuestions { get; set; }

    IEnumerable<Question> AllQuestions { get; set; }
}
