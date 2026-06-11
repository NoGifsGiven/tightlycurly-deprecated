using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources.TightlyCurly.Com;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Framework.Extensions;
using TightlyCurly.Com.Framework.Web.Utilities;
using TightlyCurly.Com.Web.Common;
using TightlyCurly.Com.Web.MasterPages;
using TightlyCurly.Com.Web.Presenters;
using TightlyCurly.Com.Web.Services;

namespace TightlyCurly.Com.Web.UserControls;

public class IngredientsControl : UserControlBase<IngredientsPresenter, IIngredientsView>, IIngredientsView
{
    protected Label chooseCategory;

    protected CategoryControl categories;

    protected GridView grdIngredients;

    public CategoryControl CategoryControl => categories;

    public IngredientsControl()
    {
        Initialize(this);
    }

    protected override void OnLoad(EventArgs e)
    {
        categories.Categories = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.SearchCategories;
        DataBindView();
        ((Control)this).OnLoad(e);
    }

    protected override void OnInit(EventArgs e)
    {
        InitializeEvents();
        ((UserControl)this).OnInit(e);
    }

    private void InitializeEvents()
    {
        categories.ItemSelected += ItemSelected;
    }

    private void ItemSelected(object sender, ItemSelectedEventArgs e)
    {
        DataBindView(base.Presenter.GetIngredients((TightlyCurly.Com.Web.Services.Ingredient i) => i.Title.StartsWith(e.SelectedItem)));
    }

    private Func<TightlyCurly.Com.Web.Services.Ingredient, bool> ParseRouteData()
    {
        string title = GetIngredientTitle(((Control)this).Page.RouteData.Values["ingredientName"].ToString());
        return (TightlyCurly.Com.Web.Services.Ingredient i) => i.Title == title;
    }

    private string GetIngredientTitle(string ingredientName)
    {
        IUrlEntryDataProvider urlEntryDataProvider = Container.Resolve<IUrlEntryDataProvider>();
        IIngredientDataProvider ingredientDataProvider = Container.Resolve<IIngredientDataProvider>();
        UrlEntry urlEntryByKey = urlEntryDataProvider.GetUrlEntryByKey(ingredientName);
        if (urlEntryByKey == null)
        {
            return null;
        }
        TightlyCurly.Com.Common.Model.Ingredient ingredientById = ingredientDataProvider.GetIngredientById(urlEntryByKey.ObjectId);
        return ingredientById.Title;
    }

    private Func<TightlyCurly.Com.Web.Services.Ingredient, bool> ParseQueryString(NameValueCollection queryStrings)
    {
        if (!queryStrings.HasKeys())
        {
            return null;
        }
        Func<TightlyCurly.Com.Web.Services.Ingredient, bool> result = null;
        int ingredientId = 0;
        if (queryStrings["ingredientId"] != null && int.TryParse(queryStrings["ingredientId"], out ingredientId))
        {
            result = (TightlyCurly.Com.Web.Services.Ingredient i) => i.IngredientId == ingredientId;
        }
        else if (queryStrings["ingredientName"] != null && !string.IsNullOrEmpty(queryStrings["ingredientName"]))
        {
            result = (TightlyCurly.Com.Web.Services.Ingredient i) => i.Title.ToLower().StartsWith(TextEncoder.SafeEncode(queryStrings["ingredientName"].ToLower()));
        }
        return result;
    }

    private void DataBindView()
    {
        Func<TightlyCurly.Com.Web.Services.Ingredient, bool> func = null;
        func = ((!((Control)this).Page.RouteData.Values.ContainsKey("ingredientName") || string.IsNullOrEmpty(((Control)this).Page.RouteData.Values["ingredientName"].ToString())) ? ParseQueryString(((UserControl)this).Request.QueryString) : ParseRouteData());
        if (func != null)
        {
            DataBindView(base.Presenter.GetIngredients(func));
        }
    }

    private void DataBindView(IEnumerable<TightlyCurly.Com.Web.Services.Ingredient> ingredients)
    {
        ((BaseDataBoundControl)grdIngredients).DataSource = (ingredients.IsNullOrEmpty() ? Enumerable.Empty<TightlyCurly.Com.Web.Services.Ingredient>() : ingredients.OrderBy((TightlyCurly.Com.Web.Services.Ingredient i) => i.Title, new IngredientComparer()));
        ((Control)grdIngredients).DataBind();
        if (ingredients != null && ingredients.Any())
        {
            SetTitle(ingredients);
        }
    }

    private void SetTitle(IEnumerable<TightlyCurly.Com.Web.Services.Ingredient> ingredients)
    {
        Master master = ((Control)this).Page.Master as Master;
        string empty = string.Empty;
        Ingredients ingredients2 = ((Control)this).Page as Ingredients;
        if (ingredients.Count() == 1)
        {
            empty = ingredients.First().Title;
            if (master != null)
            {
                master.PageTitle = empty;
            }
        }
        else
        {
            empty = ingredients.First().Title.Substring(0, 1);
            if (master != null)
            {
                master.PageTitle = string.Format(Resources.TightlyCurly.Com.Web.ingredientsStartingWith, string.Format(Resources.TightlyCurly.Com.Web.formattedSpan, empty));
            }
            empty = string.Format(Resources.TightlyCurly.Com.Web.ingredientsStartingWith, empty);
        }
        ((Control)this).Page.Title = empty;
        if (ingredients2 != null)
        {
            ((Control)ingredients2.Preamble).Visible = string.IsNullOrEmpty(empty);
        }
    }

    protected void grdIngredients_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //IL_0035: Unknown result type (might be due to invalid IL or missing references)
        //IL_003f: Expected O, but got Unknown
        //IL_0052: Unknown result type (might be due to invalid IL or missing references)
        //IL_005c: Expected O, but got Unknown
        //IL_006f: Unknown result type (might be due to invalid IL or missing references)
        //IL_0079: Expected O, but got Unknown
        //IL_008c: Unknown result type (might be due to invalid IL or missing references)
        //IL_00a1: Unknown result type (might be due to invalid IL or missing references)
        //IL_00ab: Expected O, but got Unknown
        //IL_00ab: Expected O, but got Unknown
        //IL_00be: Unknown result type (might be due to invalid IL or missing references)
        //IL_00d3: Unknown result type (might be due to invalid IL or missing references)
        //IL_00dd: Expected O, but got Unknown
        //IL_00dd: Expected O, but got Unknown
        if (e.Row.DataItem is TightlyCurly.Com.Web.Services.Ingredient ingredient)
        {
            FormatTitle(ingredient, (Label)((Control)e.Row).FindControl("title"));
            FormatAlias(ingredient, (Label)((Control)e.Row).FindControl("alias"));
            FormatRating(ingredient, (Label)((Control)e.Row).FindControl("ingredientRating"));
            FormatReferences(ingredient, (Control)(Label)((Control)e.Row).FindControl("relatedLinksLabel"), (Literal)((Control)e.Row).FindControl("relatedLinks"));
            FormatExternalLinks(ingredient, (Label)((Control)e.Row).FindControl("externalLinksLabel"), (Literal)((Control)e.Row).FindControl("externalLinks"));
        }
    }

    private void FormatTitle(TightlyCurly.Com.Web.Services.Ingredient ingredient, Label label)
    {
        label.Text = ingredient.Title;
    }

    private void FormatAlias(TightlyCurly.Com.Web.Services.Ingredient ingredient, Label label)
    {
        if (!string.IsNullOrEmpty(ingredient.Alias))
        {
            ((Control)label).Visible = true;
            label.Text = string.Format(Resources.TightlyCurly.Com.Web.alsoKnownAs, DataBinder.Eval((object)ingredient, "Alias"));
        }
    }

    private void FormatRating(TightlyCurly.Com.Web.Services.Ingredient ingredient, Label label)
    {
        if (ingredient.IngredientRating.HasValue)
        {
            switch (ingredient.IngredientRating)
            {
                case 1:
                    label.Text = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.AvoidText;
                    ((WebControl)label).CssClass = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.AvoidStyle;
                    break;
                case 2:
                    label.Text = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.CautionText;
                    ((WebControl)label).CssClass = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.CautionStyle;
                    break;
                case 3:
                    label.Text = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.GoodText;
                    ((WebControl)label).CssClass = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.GoodStyle;
                    break;
                default:
                    label.Text = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.AcceptableText;
                    ((WebControl)label).CssClass = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.AcceptableStyle;
                    break;
            }
        }
        else
        {
            label.Text = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.AcceptableText;
            ((WebControl)label).CssClass = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.AcceptableStyle;
        }
    }

    private void FormatReferences(TightlyCurly.Com.Web.Services.Ingredient ingredient, Control related, Literal references)
    {
        if (!string.IsNullOrEmpty(ingredient.InternalLinks))
        {
            related.Visible = true;
            ((Control)references).Visible = true;
            string[] array = ingredient.InternalLinks.Split(new char[1] { ',' });
            StringBuilder stringBuilder = new StringBuilder();
            string[] array2 = array;
            foreach (string text in array2)
            {
                stringBuilder.Append("<a href=\"" + TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.LinkUrl + "?ingredientName=" + text + "\">" + text + "</a> ");
            }
            references.Text += stringBuilder.ToString();
            references.Text += "<br />";
        }
    }

    private void FormatExternalLinks(TightlyCurly.Com.Web.Services.Ingredient ingredient, Label externalLinksText, Literal externalLinks)
    {
        if (string.IsNullOrEmpty(ingredient.ExternalLinks))
        {
            return;
        }
        ((Control)externalLinksText).Visible = true;
        ((Control)externalLinks).Visible = true;
        string[] array = ingredient.ExternalLinks.Split(new char[1] { ',' });
        StringBuilder stringBuilder = new StringBuilder();
        string[] array2 = array;
        foreach (string text in array2)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string text2 = string.Empty;
                string text3 = text;
                if (text3.StartsWith("#"))
                {
                    text2 = text3.Replace("#", string.Empty);
                    text3 = TightlyCurly.Com.Web.Common.Constants.IngredientsConstants.LinkUrl + text3;
                }
                stringBuilder.Append("<a href=\"" + text3 + "\">" + ((text2 == string.Empty) ? text3 : text2) + "</a> ");
            }
        }
        externalLinks.Text += stringBuilder.ToString();
        externalLinks.Text += "<br />";
    }
}
