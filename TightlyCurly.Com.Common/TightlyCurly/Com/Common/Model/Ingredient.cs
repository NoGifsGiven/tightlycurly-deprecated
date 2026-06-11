using System;

namespace TightlyCurly.Com.Common.Model;

public class Ingredient : IModelEntity
{
    [System.ComponentModel.DataAnnotations.Key]
    public int IngredientId { get; set; }

    public string Title { get; set; }

    public string Alias { get; set; }

    public string Description { get; set; }

    public string InternalLinks { get; set; }

    public string ExternalLinks { get; set; }

    public int? IngredientRating { get; set; }

    public int LocaleId { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public Locale Locale { get; set; }
}
