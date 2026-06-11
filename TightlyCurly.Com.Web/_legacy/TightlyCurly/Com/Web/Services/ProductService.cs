using System.Data.Services;
using System.ServiceModel;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Services;

[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class ProductService : DataService<TightlyCurly.Com.Common.Model.DataEntitiesDataContext>
{
    protected override TightlyCurly.Com.Common.Model.DataEntitiesDataContext CreateDataSource()
    {
        DataEntitiesDataContextWrapper dataEntitiesDataContextWrapper = new DataEntitiesDataContextWrapper(Container.Resolve<IConfigurationHelper>());
        return dataEntitiesDataContextWrapper.Context;
    }

    public static void InitializeService(IDataServiceConfiguration config)
    {
        config.UseVerboseErrors = true;
        config.SetEntitySetAccessRule("Ingredients", (EntitySetRights)3);
    }
}
