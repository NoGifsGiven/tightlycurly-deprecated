using System;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Helpers;

public class UriModelConverter : IModelConverter<UrlEntry>
{
    protected IFormatter<string, string> _formatter;

    protected IUrlEntryDataProvider _urlEntryDataProvider;

    protected IConfigurationHelper _configurationHelper;

    public UriModelConverter(IFormatter<string, string> seoFriendlyFormatter, IUrlEntryDataProvider urlEntryDataProvider, IConfigurationHelper configurationHelper)
    {
        if (seoFriendlyFormatter == null)
        {
            throw new ArgumentNullException("seoFriendlyFormatter");
        }
        if (urlEntryDataProvider == null)
        {
            throw new ArgumentNullException("urlEntryDataProvider");
        }
        if (configurationHelper == null)
        {
            throw new ArgumentNullException("configurationHelper");
        }
        _formatter = seoFriendlyFormatter;
        _urlEntryDataProvider = urlEntryDataProvider;
        _configurationHelper = configurationHelper;
    }

    public UrlEntry Convert(IModelEntity value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }
        if ((object)value.GetType() == typeof(Ingredient))
        {
            return ConvertIngredient(value as Ingredient);
        }
        if ((object)value.GetType() == typeof(Question))
        {
            return ConvertQuestion(value as Question);
        }
        if ((object)value.GetType() == typeof(Page))
        {
            return ConvertPage(value as Page);
        }
        return null;
    }

    private UrlEntry ConvertPage(Page page)
    {
        UrlEntry urlEntry = _urlEntryDataProvider.GetUrlEntryByObjectTypeAndObjectId(ObjectType.Page, page.PageId) ?? new UrlEntry();
        urlEntry.Key = string.Format(_formatter.Format(page.Name), new object[0]);
        urlEntry.Uri = page.Uri;
        urlEntry.ObjectType = ObjectType.Page;
        urlEntry.ObjectId = page.PageId;
        urlEntry.ChangeFrequency = ChangeFrequency.Monthly;
        urlEntry.Priority = "High";
        return urlEntry;
    }

    private UrlEntry ConvertQuestion(Question question)
    {
        UrlEntry urlEntry = _urlEntryDataProvider.GetUrlEntryByObjectTypeAndObjectId(ObjectType.Question, question.QuestionId) ?? new UrlEntry();
        urlEntry.Key = string.Format(_formatter.Format(question.Value), new object[0]);
        urlEntry.Uri = new Uri($"{_configurationHelper.BaseSiteUrl}{_configurationHelper.QuestionsUrlPrefix}{_formatter.Format(question.Value)}", UriKind.RelativeOrAbsolute);
        urlEntry.ObjectType = ObjectType.Question;
        urlEntry.ObjectId = question.QuestionId;
        urlEntry.ChangeFrequency = _configurationHelper.QuestionsUrlChangeFrequency;
        urlEntry.Priority = _configurationHelper.QuestionsUrlPriority;
        return urlEntry;
    }

    private UrlEntry ConvertIngredient(Ingredient ingredient)
    {
        UrlEntry urlEntry = _urlEntryDataProvider.GetUrlEntryByObjectTypeAndObjectId(ObjectType.Ingredient, ingredient.IngredientId) ?? new UrlEntry();
        urlEntry.Key = string.Format(_formatter.Format(ingredient.Title), new object[0]);
        urlEntry.Uri = new Uri($"{_configurationHelper.BaseSiteUrl}{_configurationHelper.IngredientsUrlPrefix}{_formatter.Format(ingredient.Title)}", UriKind.RelativeOrAbsolute);
        urlEntry.ObjectType = ObjectType.Ingredient;
        urlEntry.ObjectId = ingredient.IngredientId;
        urlEntry.ChangeFrequency = _configurationHelper.IngredientsUrlChangeFrequency;
        urlEntry.Priority = _configurationHelper.IngredientsUrlPriority;
        return urlEntry;
    }
}
