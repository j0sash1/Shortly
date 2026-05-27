using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shortly.Application.Interfaces;

namespace Shortly.Pages.r;

public class shortUrlModel : PageModel
{
    private readonly ILinkService _linkService;

    public shortUrlModel(ILinkService linkService)
    {
        _linkService = linkService;
    }

    public async Task<IActionResult> OnGetAsync(string shortUrl)
    {
        var link = await _linkService.GetLink(shortUrl);

        return Redirect(link.Url);
    }
}