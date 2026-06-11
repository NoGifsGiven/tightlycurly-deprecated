using System;
using System.Collections.Generic;
using System.Linq;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Web.Services;

namespace TightlyCurly.Com.Web.Presenters;

public class IngredientsPresenter : IPresenter
{
    private IIngredientsView _view;

    private IConfigurationHelper _configuration;

    public IngredientsPresenter(IIngredientsView view, IConfigurationHelper configuration)
    {
        _view = view;
        _configuration = configuration;
    }

    public IEnumerable<Ingredient> GetIngredients(Func<Ingredient, bool> filter)
    {
        string productService = _configuration.ProductService;
        DataEntitiesDataContext dataEntitiesDataContext = new DataEntitiesDataContext(new Uri(productService, UriKind.Absolute));
        return from i in ((IQueryable<Ingredient>)dataEntitiesDataContext.Ingredients).Select((Ingredient i) => i).Where(filter)
            orderby i.Title
            select i;
    }
}
