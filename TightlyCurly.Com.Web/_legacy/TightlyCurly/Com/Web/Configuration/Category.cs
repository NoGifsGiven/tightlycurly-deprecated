using System.Configuration;

namespace TightlyCurly.Com.Web.Configuration;

public class Category : ConfigurationElement
{
    [ConfigurationProperty("categoryName")]
    public string CategoryName => ((ConfigurationElement)this)["categoryName"].ToString();

    [ConfigurationProperty("arguments")]
    public string Arguments => ((ConfigurationElement)this)["arguments"].ToString();
}
