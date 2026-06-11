using System;
using System.Collections.Generic;

namespace TightlyCurly.Com.Common.Model;

public class MenuItem
{
    public int MenuItemId { get; set; }

    public string Text { get; set; }

    public string Description { get; set; }

    public Uri Url { get; set; }

    public int Order { get; set; }

    public IEnumerable<MenuItem> Children { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public MenuItem()
    {
        Children = new List<MenuItem>();
    }
}
