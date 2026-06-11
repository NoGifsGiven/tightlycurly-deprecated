using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources.TightlyCurly.Com;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Extensions;
using TightlyCurly.Com.Web.Presenters;
using TightlyCurly.Com.Web.UserControls;

namespace TightlyCurly.Com.Web;

public class AnswerBank : PageBase<AnswerBankPresenter, IAnswerBankView>, IAnswerBankView
{
    private const string QuestionId = "questionId";

    protected Label AllQuestionsLabel;

    protected ListBox AllQuestionsList;

    protected Button ViewSelectedQuestion;

    protected SearchControl QuestionSearchControl;

    protected Panel TopicsContainer;

    protected Label MasterCategoriesLabel;

    protected DropDownList MasterCategoriesList;

    protected Panel SubcategoriesListContainer;

    protected Label SubcategoriesLabel;

    protected DropDownList SubcategoriesList;

    protected Button ViewQuestions;

    protected GridView QuestionsList;

    public IEnumerable<QuestionCategory> MasterCategories
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            ((BaseDataBoundControl)MasterCategoriesList).DataSource = value;
            ((ListControl)MasterCategoriesList).DataValueField = "QuestionCategoryId";
            ((ListControl)MasterCategoriesList).DataTextField = "Category";
            ((Control)MasterCategoriesList).DataBind();
        }
    }

    public IEnumerable<QuestionCategory> Subcategories
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            //IL_0024: Unknown result type (might be due to invalid IL or missing references)
            //IL_002e: Expected O, but got Unknown
            if (value.IsNullOrEmpty())
            {
                ((ListControl)SubcategoriesList).Items.Add(new ListItem("Please select a master category.", "0"));
                return;
            }
            ((ListControl)SubcategoriesList).Items.Clear();
            ((BaseDataBoundControl)SubcategoriesList).DataSource = value;
            ((ListControl)SubcategoriesList).DataValueField = "QuestionCategoryID";
            ((ListControl)SubcategoriesList).DataTextField = "Category";
            ((Control)SubcategoriesList).DataBind();
        }
    }

    public IEnumerable<Question> SelectedQuestions
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            ((BaseDataBoundControl)QuestionsList).DataSource = value;
            ((Control)QuestionsList).DataBind();
        }
    }

    public IEnumerable<Question> AllQuestions
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            ((ListControl)AllQuestionsList).DataValueField = "QuestionId";
            ((ListControl)AllQuestionsList).DataTextField = "Value";
            ((BaseDataBoundControl)AllQuestionsList).DataSource = value ?? Enumerable.Empty<Question>();
            ((Control)AllQuestionsList).DataBind();
        }
    }

    public AnswerBank()
    {
        Initialize(this);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!((Page)this).IsPostBack)
        {
            DataBindMasterCategories();
            DataBindAllQuestions();
            DataBindSelectedQuestion();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        QuestionSearchControl.ServicePath = _configuration.AutoCompleteService;
        QuestionSearchControl.ServiceMethod = _configuration.QuestionListServiceMethod;
        QuestionSearchControl.AutoCompleteEnabled = true;
        InitializeEvents();
        ((Page)this).OnInit(e);
    }

    private void InitializeEvents()
    {
        QuestionSearchControl.SearchSelected += SearchSelected;
    }

    private void SearchSelected(object sender, SearchEventArgs e)
    {
        DataBindQuestions(e.SearchItem);
    }

    protected void MasterCategoriesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((ListControl)MasterCategoriesList).SelectedIndex > -1)
        {
            DataBindSubcategories(int.Parse(((ListControl)MasterCategoriesList).SelectedValue));
        }
    }

    protected void ViewQuestions_Clicked(object sender, EventArgs e)
    {
        if (((ListControl)SubcategoriesList).SelectedIndex >= 0 && int.Parse(((ListControl)SubcategoriesList).SelectedValue) > 0)
        {
            DataBindQuestions(int.Parse(((ListControl)SubcategoriesList).SelectedValue));
        }
    }

    protected void ViewSelectedQuestion_Click(object sender, EventArgs e)
    {
        if (((ListControl)AllQuestionsList).SelectedIndex >= 0)
        {
            base.Presenter.GetQuestionById(int.Parse(((ListControl)AllQuestionsList).SelectedValue));
        }
        else
        {
            base.MessageBox.Show("Please select a question.");
        }
    }

    private void DataBindAllQuestions()
    {
        base.Presenter.GetAllQuestions();
    }

    private void DataBindMasterCategories()
    {
        base.Presenter.GetMasterCategories();
    }

    private void DataBindSelectedQuestion()
    {
        int questionId = GetQuestionId();
        if (questionId > 0)
        {
            base.Presenter.GetQuestionById(questionId);
        }
    }

    private int GetQuestionId()
    {
        int result = 0;
        if (!string.IsNullOrEmpty(((Page)this).Request.QueryString["questionId"]))
        {
            int.TryParse(((Page)this).Request.QueryString["questionId"], out result);
        }
        else if (((Control)this).Page.RouteData.Values.ContainsKey("questionName") && !string.IsNullOrEmpty(((Control)this).Page.RouteData.Values["questionName"].ToString()))
        {
            result = ParseRouteData(((Control)this).Page.RouteData.Values["questionName"].ToString());
        }
        return result;
    }

    private int ParseRouteData(string questionName)
    {
        IUrlEntryDataProvider urlEntryDataProvider = Container.Resolve<IUrlEntryDataProvider>();
        IQuestionDataProvider questionDataProvider = Container.Resolve<IQuestionDataProvider>();
        UrlEntry urlEntryByKey = urlEntryDataProvider.GetUrlEntryByKey(questionName);
        if (urlEntryByKey == null)
        {
            return 0;
        }
        return questionDataProvider.GetQuestionById(urlEntryByKey.ObjectId)?.QuestionId ?? 0;
    }

    private void DataBindSubcategories(int parentCategoryId)
    {
        //IL_001a: Unknown result type (might be due to invalid IL or missing references)
        //IL_0020: Expected O, but got Unknown
        base.Presenter.GetSubcategoriesByParentId(parentCategoryId);
        ListItemCollection items = ((ListControl)SubcategoriesList).Items;
        ListItem val = new ListItem();
        val.Value = "0";
        val.Text = Resources.TightlyCurly.Com.Web.selectSubTopicText;
        items.Insert(0, val);
    }

    private void DataBindQuestions(string searchText)
    {
        base.Presenter.GetQuestionsByCriteria(searchText);
    }

    private void DataBindQuestions(int subcategoryId)
    {
        base.Presenter.GetQuestionsBySubcategoryId(subcategoryId);
    }
}
