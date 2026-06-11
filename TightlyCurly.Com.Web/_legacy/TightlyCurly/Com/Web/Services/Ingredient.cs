using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace TightlyCurly.Com.Web.Services;

[DataServiceKey("IngredientId")]
public class Ingredient
{
    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _IngredientId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _Title;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _Alias;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _Description;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _InternalLinks;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private string _ExternalLinks;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int? _IngredientRating;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private int _LocaleId;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _UpdatedDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    private DateTime _EnteredDate;

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int IngredientId
    {
        get
        {
            return _IngredientId;
        }
        set
        {
            _IngredientId = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public string Title
    {
        get
        {
            return _Title;
        }
        set
        {
            _Title = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public string Alias
    {
        get
        {
            return _Alias;
        }
        set
        {
            _Alias = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public string Description
    {
        get
        {
            return _Description;
        }
        set
        {
            _Description = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public string InternalLinks
    {
        get
        {
            return _InternalLinks;
        }
        set
        {
            _InternalLinks = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public string ExternalLinks
    {
        get
        {
            return _ExternalLinks;
        }
        set
        {
            _ExternalLinks = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int? IngredientRating
    {
        get
        {
            return _IngredientRating;
        }
        set
        {
            _IngredientRating = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public int LocaleId
    {
        get
        {
            return _LocaleId;
        }
        set
        {
            _LocaleId = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DateTime UpdatedDate
    {
        get
        {
            return _UpdatedDate;
        }
        set
        {
            _UpdatedDate = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public DateTime EnteredDate
    {
        get
        {
            return _EnteredDate;
        }
        set
        {
            _EnteredDate = value;
        }
    }

    [GeneratedCode("System.Data.Services.Design", "1.0.0")]
    public static Ingredient CreateIngredient(int ingredientId, int localeId, DateTime updatedDate, DateTime enteredDate)
    {
        Ingredient ingredient = new Ingredient();
        ingredient.IngredientId = ingredientId;
        ingredient.LocaleId = localeId;
        ingredient.UpdatedDate = updatedDate;
        ingredient.EnteredDate = enteredDate;
        return ingredient;
    }
}
