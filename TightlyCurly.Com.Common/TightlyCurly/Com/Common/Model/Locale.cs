using System;
using System.Collections.Generic;

namespace TightlyCurly.Com.Common.Model;

public class Locale
{
    [System.ComponentModel.DataAnnotations.Key]
    public int LocaleId { get; set; }

    public int LCID { get; set; }

    public string LocaleName { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public byte[] TimeStamp { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new();
}
