using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewAPI.Authorization;
using ReviewAPI.DTOs.Steam;
using ReviewAPI.Models;

namespace ReviewAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SteamAppController : ControllerBase
    {
        private readonly GameReviewContext _context;

        public SteamAppController(GameReviewContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<SteamApp>>> SearchGames
            (
            [FromQuery] string? search = null,
            [FromQuery] List<int>? categories = null,
            [FromQuery] List<string>? genres = null,
            [FromQuery] List<string>? publishers = null,
            [FromQuery] List<string>? developers = null,
            [FromQuery] int page = 1,
            [FromQuery] int appCount = 30
            )
        {
            var query = _context.SteamApps.AsQueryable();

            if (categories != null)
            {
                query = query.Where(c => categories.All(cat => c.SteamAppCategory.Any(ac => ac.CategoryId == cat)));
            }

            if (genres != null)
            {
                query = query.Where(q => genres.All(genre => q.SteamAppGenre.Any(ag => ag.GenreId == genre)));
            }

            if (publishers != null)
            {
                query = query.Where(p => publishers.All(pub => p.SteamAppPublisher.Any(ap => ap.SteamAppPublisher.Publisher.Contains(pub))));
            }

            if (developers != null)
            {
                query = query.Where(d => developers.All(dev => d.SteamAppDeveloper.Any(ad => ad.SteamAppDeveloper.Developer.Contains(dev))));
            }

            if (search != null)
            {
                query = query.Where(a => a.Name.Contains(search))
                    .OrderByDescending(a => a.Name.StartsWith(search))
                    .ThenByDescending(a => a.OwnersAvg);
            }
            else
            {
                query = query.OrderByDescending(a => a.OwnersAvg);
            }

            query = query.Skip((page-1)*appCount);

            var results = await query
                .Take(appCount)
                .Select(p => new CardReturnDto 
                { 
                    AppId = p.AppId,
                    Name = p.Name, 
                    Type = p.Type,
                    Price = p.Price,
                    RequiredAge = p.RequiredAge,
                    IsFree = p.IsFree,
                    ReleaseDate = p.ReleaseDate,
                    HeaderImage = p.HeaderImage,
                    Description = p.Description
                })
                .ToListAsync();

            return Ok(results);
        }

        [AllowAnonymous]
        [HttpGet("getGenres")]
        public async Task<ActionResult<IEnumerable<SteamAppGenre>>> GetAllGenres()
        {
            return Ok(await _context.Genres.ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet("getCategories")]
        public async Task<ActionResult<IEnumerable<SteamAppCategory>>> GetAllCategories()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet("getPublishers")]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublisher([FromQuery] string search)
        {
            var startsWith = _context.Publishers
                .Where(p => p.Publisher.StartsWith(search))
                .Take(10);

            var contains = _context.Publishers
                .Where(p => p.Publisher.Contains(search) && !p.Publisher.StartsWith(search))
                .Take(10);

            var results = await startsWith.Concat(contains)
                .Take(10)
                .Select(p => new PublisherDto { PublisherId = p.PublisherId, Publisher = p.Publisher})
                .ToListAsync();

            return Ok(results);
        }

        [AllowAnonymous]
        [HttpGet("getDevelopers")]
        public async Task<ActionResult<IEnumerable<DeveloperDto>>> GetDeveloper([FromQuery] string search)
        {
            var startsWith = _context.Developers
                .Where(p => p.Developer.StartsWith(search))
                .Take(10);

            var contains = _context.Developers
                .Where(p => p.Developer.Contains(search) && !p.Developer.StartsWith(search))
                .Take(10);

            var results = await startsWith.Concat(contains)
                .Take(10)
                .Select(p => new DeveloperDto { DeveloperId = p.DeveloperId, Developer = p.Developer })
                .ToListAsync();

            return Ok(results);
        }
    }
}
