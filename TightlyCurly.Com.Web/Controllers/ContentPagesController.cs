using Microsoft.AspNetCore.Mvc;
using TightlyCurly.Com.Common;
using TightlyCurly.Com.Common.Model;
using TightlyCurly.Com.Web.Helpers;
using TightlyCurly.Com.Web.Services;

namespace TightlyCurly.Com.Web.Controllers;

/// <summary>
/// Replaces ContentPagePresenter. Instead of pushing content into an
/// IContentPageView and signalling errors through IHttpContextHelper, the page
/// content comes back as a POCO and the ViewStatus becomes the HTTP status code.
/// </summary>
[ApiController]
[Route("api/pages")]
public class ContentPagesController : ControllerBase
{
    private readonly IPageService _pageService;

    private readonly IResourceHelper _resourceHelper;

    public ContentPagesController(IPageService pageService, IResourceHelper resourceHelper)
    {
        _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        _resourceHelper = resourceHelper ?? throw new ArgumentNullException(nameof(resourceHelper));
    }

    [HttpGet("{pageName}")]
    public ActionResult<ContentPage> GetPageByName(string pageName)
    {
        if (string.IsNullOrEmpty(pageName))
        {
            return BadRequest("pageName is required.");
        }

        Page selectedPage;
        try
        {
            selectedPage = _pageService.GetPageByName(pageName);
        }
        catch (InvalidOperationException)
        {
            // Bad page data (e.g. multiple active content rows); formerly surfaced
            // by the Content page as a 404.
            return NotFound();
        }

        switch (selectedPage.ViewStatus)
        {
            case ViewStatus.None:
            case ViewStatus.NotFound:
            case ViewStatus.NotAuthorized:
                return StatusCode(ViewStatusTranslator.Translate(selectedPage.ViewStatus));
            case ViewStatus.UnderConstruction:
                return Ok(UnderConstruction());
            default:
                return GetPageContent(selectedPage);
        }
    }

    private ActionResult<ContentPage> GetPageContent(Page selectedPage)
    {
        var activeContent = (selectedPage.Content ?? Enumerable.Empty<PageContent>())
            .Where(p => p.IsActive)
            .ToList();
        if (!activeContent.Any())
        {
            return Ok(UnderConstruction());
        }
        PageContent pageContent = activeContent.Single();
        return Ok(new ContentPage
        {
            Content = pageContent.Content,
            PageTitle = pageContent.Title,
            Description = pageContent.Description,
            MetaDescription = pageContent.MetaDescription,
            MetaKeywords = pageContent.MetaKeywords
        });
    }

    private ContentPage UnderConstruction()
    {
        return new ContentPage
        {
            Content = _resourceHelper.UnderConstructionContent,
            PageTitle = _resourceHelper.UnderConstructionTitle,
            Description = _resourceHelper.UnderConstructionDescription
        };
    }
}

/// <summary>The displayable content of a CMS page (formerly the IContentPageView properties).</summary>
public class ContentPage
{
    public string PageTitle { get; set; }

    public string Description { get; set; }

    public string Content { get; set; }

    public string MetaDescription { get; set; }

    public string MetaKeywords { get; set; }
}
