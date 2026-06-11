using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using TightlyCurly.Com.Common.Helpers;
using TightlyCurly.Com.Common.Model;

namespace TightlyCurly.Com.Common.Data.DataProviders;

/// <summary>
/// Replaces LinqIngredientDataProvider (EF Core, originally LINQ to SQL): the same
/// queries against the TIGHTLY_CURLY database, issued through Dapper.
/// </summary>
public class DapperIngredientDataProvider : IIngredientDataProvider
{
    private const string SelectColumns =
        "IngredientId, Title, Alias, Description, InternalLinks, ExternalLinks, IngredientRating, LocaleId, UpdatedDate, EnteredDate";

    private readonly IConfigurationHelper _configurationHelper;

    public DapperIngredientDataProvider(IConfigurationHelper configurationHelper)
    {
        _configurationHelper = configurationHelper ?? throw new ArgumentNullException(nameof(configurationHelper));
    }

    private SqlConnection CreateConnection()
    {
        return new SqlConnection(_configurationHelper.DefaultConnectionString);
    }

    public IEnumerable<Ingredient> GetAllIngredients()
    {
        using var connection = CreateConnection();
        return connection.Query<Ingredient>($"SELECT {SelectColumns} FROM dbo.Ingredients").ToList();
    }

    public Ingredient GetIngredientById(int ingredientId)
    {
        using var connection = CreateConnection();
        return connection.QuerySingleOrDefault<Ingredient>(
            $"SELECT {SelectColumns} FROM dbo.Ingredients WHERE IngredientId = @IngredientId",
            new { IngredientId = ingredientId });
    }

    public Ingredient GetIngredientByName(string ingredientName)
    {
        using var connection = CreateConnection();
        return connection.QueryFirstOrDefault<Ingredient>(
            $"SELECT {SelectColumns} FROM dbo.Ingredients WHERE Title = @Title",
            new { Title = ingredientName });
    }

    public Ingredient SaveIngredient(Ingredient ingredient)
    {
        using var connection = CreateConnection();
        if (ingredient.IngredientId == 0)
        {
            ingredient.EnteredDate = DateTimeOffset.UtcNow;
            ingredient.UpdatedDate = DateTimeOffset.UtcNow;

            // Assign the default locale (formerly the LCID 1033 lookup).
            int? localeId = connection.QueryFirstOrDefault<int?>(
                "SELECT LocaleId FROM dbo.Locales WHERE LCID = 1033");
            if (localeId.HasValue)
            {
                ingredient.LocaleId = localeId.Value;
            }

            ingredient.IngredientId = connection.ExecuteScalar<int>(
                @"INSERT INTO dbo.Ingredients
                      (Title, Alias, Description, InternalLinks, ExternalLinks, IngredientRating, LocaleId, UpdatedDate, EnteredDate)
                  VALUES
                      (@Title, @Alias, @Description, @InternalLinks, @ExternalLinks, @IngredientRating, @LocaleId, @UpdatedDate, @EnteredDate);
                  SELECT CAST(SCOPE_IDENTITY() AS int);",
                ingredient);
        }
        else
        {
            connection.Execute(
                @"UPDATE dbo.Ingredients
                  SET Title = @Title,
                      Alias = @Alias,
                      Description = @Description,
                      InternalLinks = @InternalLinks,
                      ExternalLinks = @ExternalLinks,
                      IngredientRating = @IngredientRating,
                      UpdatedDate = @UpdatedDate
                  WHERE IngredientId = @IngredientId",
                new
                {
                    ingredient.Title,
                    ingredient.Alias,
                    ingredient.Description,
                    ingredient.InternalLinks,
                    ingredient.ExternalLinks,
                    ingredient.IngredientRating,
                    UpdatedDate = DateTimeOffset.UtcNow,
                    ingredient.IngredientId
                });
        }
        return ingredient;
    }

    public void DeleteIngredient(Ingredient ingredient)
    {
        using var connection = CreateConnection();
        connection.Execute(
            "DELETE FROM dbo.Ingredients WHERE IngredientId = @IngredientId",
            new { ingredient.IngredientId });
    }
}
