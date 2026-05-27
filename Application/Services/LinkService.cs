using Microsoft.EntityFrameworkCore;
using Shortly.Application.Interfaces;
using Shortly.Domain.Entities;
using Shortly.Infrastructure.Persistence;

namespace Shortly.Application.Services;

public sealed class LinkService : ILinkService
{
    private readonly ILogger<LinkService> _logger;
    private readonly AppDbContext _context;

    public LinkService(ILogger<LinkService> logger, AppDbContext context)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Creates a new shortened link.
    /// </summary>
    public async Task<Link> CreateLink(Link link)
    {
        if (link == null)
        {
            _logger.LogError("CreateLink failed: Link object is null.");
            throw new ArgumentNullException(nameof(link), "Link cannot be null.");
        }

        _logger.LogDebug("Attempting to create link for URL: {Url}", link.Url);

        var existLink = await _context.Links
            .AsNoTracking()
            .AnyAsync(l => l.ShortUrl == link.ShortUrl);

        if (existLink)
        {
            _logger.LogError("CreateLink failed: ShortUrl {ShortUrl} already exists.", link.ShortUrl);
            throw new InvalidOperationException("ShortUrl is already in use.");
        }

        _context.Links.Add(link);
        await _context.SaveChangesAsync();

        _logger.LogDebug("Link created successfully with ShortUrl: {ShortUrl}", link.ShortUrl);
        return link;
    }

    /// <summary>
    /// Increments the click count for a given short URL and user ID.
    /// </summary>
    public async Task<Link> IncrementClicks(string shortUrl, long userId)
    {
        _logger.LogDebug("Incrementing clicks for ShortUrl: {ShortUrl}, UserId: {UserId}", shortUrl, userId);

        var link = await _context.Links
            .FirstOrDefaultAsync(l => l.ShortUrl == shortUrl && l.UserId == userId);

        if (link == null)
        {
            _logger.LogError("IncrementClicks failed: No link found with ShortUrl {ShortUrl} for UserId {UserId}.", shortUrl, userId);
            throw new InvalidOperationException("Link not found.");
        }

        link.IncrementClicks();
        await _context.SaveChangesAsync();

        _logger.LogDebug("Clicks incremented for ShortUrl: {ShortUrl}. Total clicks: {Clicks}", shortUrl, link.Clicks);
        return link;
    }

    /// <summary>
    /// Retrieves a link by its short URL.
    /// </summary>
    public async Task<Link> GetLink(string shortUrl)
    {
        _logger.LogDebug("Retrieving link with ShortUrl: {ShortUrl}", shortUrl);

        var link = await _context.Links
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.ShortUrl == shortUrl);

        if (link == null)
        {
            _logger.LogError("GetLink failed: No link found with ShortUrl {ShortUrl}.", shortUrl);
            throw new InvalidOperationException("Link not found.");
        }

        return link;
    }

    /// <summary>
    /// Retrieves all links in the system.
    /// </summary>
    public async Task<List<Link>> GetAllLinks()
    {
        _logger.LogDebug("Retrieving all links from the database.");
        var links = await _context.Links.AsNoTracking().ToListAsync();
        _logger.LogDebug("Retrieved {LinkCount} links from the database.", links.Count);
        return links;
    }
}