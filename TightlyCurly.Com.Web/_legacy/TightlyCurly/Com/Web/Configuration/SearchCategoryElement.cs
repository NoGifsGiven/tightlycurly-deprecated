using System.Configuration;

namespace TightlyCurly.Com.Web.Configuration;

public class SearchCategoryElement : ConfigurationElement
{
    [ConfigurationCollection(typeof(CategoryElementCollection), AddItemName = "category")]
    [ConfigurationProperty("categories")]
    public CategoryElementCollection Categories => (CategoryElementCollection)((ConfigurationElement)this)["categories"];
}
