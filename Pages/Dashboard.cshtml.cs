using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shortly.Application.Interfaces;
using Shortly.Domain.Entities;

namespace Shortly.Pages;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly ILinkService _linkService;

    public DashboardModel(ILinkService linkService)
    {
        _linkService = linkService;
    }

    [BindProperty]
    public string Url { get; set; } = string.Empty;

    public List<Link> Links { get; set; } = new();

    public async Task OnGetAsync()
    {
        Links = await _linkService.GetAllLinks();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var shortUrl = Guid.NewGuid().ToString()[..6];

        var link = new Link(Url, shortUrl, userId);

        await _linkService.CreateLink(link);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToPage("/Login");
    }
}
