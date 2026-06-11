using System.Web;
using TightlyCurly.Com.Web.Configuration;

namespace TightlyCurly.Com.Web.Common;

public static class Constants
{
    public static class ResourceConstants
    {
        public const string DefaultResource = "TightlyCurly.Com.Web";
    }

    public static class ControlConstants
    {
        public const string MessageBox = "MessageBoxControl";
    }

    public static class IngredientsConstants
    {
        public const string IngredientsNameQuery = "ingredientName";

        public const string IngredientsIdQuery = "ingredientId";

        public const char Delimiter = ',';

        public static readonly string AcceptableStyle = IngredientsSettings.AcceptableStyle;

        public static readonly string AvoidStyle = IngredientsSettings.AvoidStyle;

        public static readonly string CautionStyle = IngredientsSettings.CautionStyle;

        public static readonly string GoodStyle = IngredientsSettings.GoodStyle;

        public static readonly string LinkUrl = IngredientsSettings.LinkUrl;

        public static readonly bool AutoCompleteEnabled = IngredientsSettings.AutoCompleteEnabled;

        public static CategoryElementCollection SearchCategories = IngredientsSettings.SearchCategories;

        public static readonly string AcceptableText = (string)HttpContext.GetGlobalResourceObject("TightlyCurly.Com.Web", "acceptableText");

        public static readonly string AvoidText = (string)HttpContext.GetGlobalResourceObject("TightlyCurly.Com.Web", "avoidText");

        public static readonly string CautionText = (string)HttpContext.GetGlobalResourceObject("TightlyCurly.Com.Web", "cautionText");

        public static readonly string GoodText = (string)HttpContext.GetGlobalResourceObject("TightlyCurly.Com.Web", "goodText");
    }

    public static class ConfigurationConstants
    {
        public static class SectionConstants
        {
            public const string IngredientsSectionName = "ingredients";

            public const string SearchCategoriesSectionName = "searchCategoryElements";
        }
    }

    public static class ConnectionConstants
    {
        public static string ProductService = "ProductService";

        public static string ContentService = "ContentService";
    }

    public const string Anchor = "#";
}
