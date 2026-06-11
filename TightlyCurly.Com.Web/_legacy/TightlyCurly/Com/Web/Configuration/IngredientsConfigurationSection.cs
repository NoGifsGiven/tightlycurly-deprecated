using System.Configuration;

namespace TightlyCurly.Com.Web.Configuration;

public class IngredientsConfigurationSection : ConfigurationSection
{
    [ConfigurationProperty("avoidStyle", IsRequired = true)]
    public string AvoidStyle
    {
        get
        {
            return ((ConfigurationElement)this)["avoidStyle"].ToString();
        }
        set
        {
            ((ConfigurationElement)this)["avoidStyle"] = value;
        }
    }

    [ConfigurationProperty("cautionStyle", IsRequired = true)]
    public string CautionStyle
    {
        get
        {
            return ((ConfigurationElement)this)["cautionStyle"].ToString();
        }
        set
        {
            ((ConfigurationElement)this)["cautionStyle"] = value;
        }
    }

    [ConfigurationProperty("acceptableStyle", IsRequired = true)]
    public string AcceptableStyle
    {
        get
        {
            return ((ConfigurationElement)this)["acceptableStyle"].ToString();
        }
        set
        {
            ((ConfigurationElement)this)["acceptableStyle"] = value;
        }
    }

    [ConfigurationProperty("goodStyle", IsRequired = true)]
    public string GoodStyle
    {
        get
        {
            return ((ConfigurationElement)this)["goodStyle"].ToString();
        }
        set
        {
            ((ConfigurationElement)this)["goodStyle"] = value;
        }
    }

    [ConfigurationProperty("linkUrl", IsRequired = true)]
    public string LinkUrl
    {
        get
        {
            return ((ConfigurationElement)this)["linkUrl"].ToString();
        }
        set
        {
            ((ConfigurationElement)this)["linkUrl"] = value;
        }
    }

    [ConfigurationProperty("autoCompleteEnabled", IsRequired = true)]
    public bool AutoCompleteEnabled
    {
        get
        {
            return bool.Parse(((ConfigurationElement)this)["autoCompleteEnabled"].ToString());
        }
        set
        {
            ((ConfigurationElement)this)["autoCompleteEnabled"] = value;
        }
    }

    [ConfigurationProperty("searchCategoryElements", IsRequired = true)]
    public SearchCategoryElement SearchCategories
    {
        get
        {
            return ((ConfigurationElement)this)["searchCategoryElements"] as SearchCategoryElement;
        }
        set
        {
            ((ConfigurationElement)this)["searchCategoryElements"] = value;
        }
    }
}
