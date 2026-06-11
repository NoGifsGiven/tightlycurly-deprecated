using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Data.DataProviders;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Web.Services;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(/*Could not decode attribute arguments.*/)]
public class AutoCompleteService
{
    private readonly IConfigurationHelper _configurationHelper;

    private readonly IQuestionDataProvider _questionDataProvider;

    public AutoCompleteService()
    {
        _configurationHelper = Container.Resolve<IConfigurationHelper>();
        _questionDataProvider = Container.Resolve<IQuestionDataProvider>();
    }

    [OperationContract]
    public string[] GetIngredientsNameList(string prefixText, int count)
    {
        string productService = _configurationHelper.ProductService;
        DataEntitiesDataContext dataEntitiesDataContext = new DataEntitiesDataContext(new Uri(productService, UriKind.Absolute));
        List<Ingredient> source = ((IQueryable<Ingredient>)dataEntitiesDataContext.Ingredients).Select((Ingredient i) => i).ToList();
        IEnumerable<string> source2 = from i in source
            select i.Title into i
            where i.ToLower().Contains(prefixText.ToLower())
            select i;
        return source2.ToArray();
    }

    [OperationContract]
    public string[] GetQuestionsList(string prefixText, int count)
    {
        return (from q in _questionDataProvider.GetQuestionsByCriteria(prefixText)
            select q.Value).ToArray();
    }
}
