using System;
using System.Collections.Generic;

namespace TightlyCurly.Com.Common.Model;

public class Menu
{
    public int MenuId { get; set; }

    public string Name { get; set; }

    public IEnumerable<MenuItem> MenuItems { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public Menu()
    {
        MenuItems = new List<MenuItem>();
    }
}
