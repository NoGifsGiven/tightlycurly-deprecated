using System.Configuration;

namespace TightlyCurly.Com.Web.Configuration;

[ConfigurationCollection(typeof(Category))]
public class CategoryElementCollection : ConfigurationElementCollection
{
    public Category this[int index] => (Category)(object)((ConfigurationElementCollection)this).BaseGet(index);

    public Category this[object key] => (Category)(object)((ConfigurationElementCollection)this).BaseGet(key);

    public void Add(Category element)
    {
        ((ConfigurationElementCollection)this).BaseAdd((ConfigurationElement)(object)element);
    }

    public void Clear()
    {
        ((ConfigurationElementCollection)this).BaseClear();
    }

    public void Remove(Category element)
    {
        ((ConfigurationElementCollection)this).BaseRemove((object)element.CategoryName);
    }

    public void Remove(string name)
    {
        ((ConfigurationElementCollection)this).BaseRemove((object)name);
    }

    public void RemoveAt(int index)
    {
        ((ConfigurationElementCollection)this).BaseRemoveAt(index);
    }

    protected override ConfigurationElement CreateNewElement()
    {
        return (ConfigurationElement)(object)new Category();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
        return ((Category)(object)element).CategoryName;
    }

    protected override bool IsElementName(string elementName)
    {
        if (string.IsNullOrEmpty(elementName) || elementName != "category")
        {
            return false;
        }
        return true;
    }
}
