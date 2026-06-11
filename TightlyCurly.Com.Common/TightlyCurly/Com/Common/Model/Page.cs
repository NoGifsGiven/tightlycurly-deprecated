using System;
using System.Collections.Generic;

namespace TightlyCurly.Com.Common.Model;

public class Page : IModelEntity
{
    public int Id
    {
        get
        {
            return PageId;
        }
        set
        {
            PageId = value;
        }
    }

    public int PageId { get; set; }

    public string Name { get; set; }

    public ViewStatus ViewStatus { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public Uri Uri { get; set; }

    public IEnumerable<PageContent> Content { get; set; }

    public Page()
    {
        Content = new List<PageContent>();
    }
}
