using System.Collections.Generic;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

public interface IIngredientDataProvider
{
    IEnumerable<Ingredient> GetAllIngredients();

    Ingredient GetIngredientById(int ingredientId);

    Ingredient GetIngredientByName(string ingredientName);

    Ingredient SaveIngredient(Ingredient ingredient);

    void DeleteIngredient(Ingredient ingredient);
}
