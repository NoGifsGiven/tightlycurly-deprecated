namespace TightlyCurly.Com.Web.Presenters;

public interface IContentPageView
{
    string MetaDescription { get; set; }

    string MetaKeywords { get; set; }

    string PageTitle { get; set; }

    string Description { get; set; }

    string Content { get; set; }
}
