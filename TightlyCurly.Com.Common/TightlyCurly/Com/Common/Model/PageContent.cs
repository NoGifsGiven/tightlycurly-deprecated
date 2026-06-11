using System;

namespace TightlyCurly.Com.Common.Model;

public class PageContent
{
    public int PageContentId { get; set; }

    public string Title { get; set; }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public bool IsActive { get; set; }

    public Locale Locale { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }
}
