using System;
using System.CodeDom.Compiler;
using System.Data.Services.Client;

namespace TightlyCurly.Com.Web.Services;

public class DataEntitiesDataContext : DataServiceContext
{
    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DataServiceQuery<Ingredient> _Ingredients;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DataServiceQuery<Ingredient> Ingredients
    {
        get
        {
            if (_Ingredients == null)
            {
                _Ingredients = ((DataServiceContext)this).CreateQuery<Ingredient>("Ingredients");
            }
            return _Ingredients;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DataEntitiesDataContext(Uri serviceRoot)
        : base(serviceRoot)
    {
        ((DataServiceContext)this).ResolveName = ResolveNameFromType;
        ((DataServiceContext)this).ResolveType = ResolveTypeFromName;
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    protected Type ResolveTypeFromName(string typeName)
    {
        if (typeName.StartsWith("Alligoshee.Hair.Business", StringComparison.Ordinal))
        {
            return ((object)this).GetType().Assembly.GetType("TightlyCurly.Com.Web.Services" + typeName.Substring(24), throwOnError: false);
        }
        return null;
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    protected string ResolveNameFromType(Type clientType)
    {
        if (clientType.Namespace.Equals("TightlyCurly.Com.Web.Services", StringComparison.Ordinal))
        {
            return "Alligoshee.Hair.Business." + clientType.Name;
        }
        return null;
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public void AddToIngredients(Ingredient ingredient)
    {
        ((DataServiceContext)this).AddObject("Ingredients", (object)ingredient);
    }
}
