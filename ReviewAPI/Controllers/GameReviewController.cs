using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewAPI.Models;
using ReviewAPI.Authorization;

namespace ReviewAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameReviewController : ControllerBase
    {
        private readonly GameReviewContext _context;

        public GameReviewController(GameReviewContext context)
        {
            _context = context;
        }

        // GET: api/GameReview
        [AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetGameReviews(
            [FromQuery] int reviewCount = 15, 
            [FromQuery] int page = 1
            )
        {
            if (reviewCount <= 0) reviewCount = 1;
            if (reviewCount > 100) reviewCount = 100;
            if (page < 1) page = 1;

            IQueryable<GameReview> query = _context.GameReviews
                .OrderByDescending(gr => gr.CreatedAt)
                .ThenByDescending(gr => gr.ReviewId)
                .Skip((page - 1) * reviewCount);

            var reviews = await query.Take(reviewCount).ToListAsync();
            var total = await _context.GameReviews.CountAsync();

            return Ok(new { reviews, total });
        }

        // GET: api/GameReview/5
        [AllowAnonymous]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<GameReview>> GetGameReview(int id)
        {
            var gameReview = await _context.GameReviews.FindAsync(id);

            if (gameReview == null)
            {
                return NotFound();
            }

            return gameReview;
        }

        // PUT: api/GameReview/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutGameReview(int id, GameReview gameReview)
        {

            if (id != gameReview.ReviewId)
            {
                return BadRequest();
            }

            var existingReview = await _context.GameReviews.FindAsync(id);

            if (existingReview == null)
            {
                return NotFound();
            }

            existingReview.Rating = gameReview.Rating;
            existingReview.Content = gameReview.Content;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameReview
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = Roles.User)]
        [HttpPost("create")]
        public async Task<ActionResult<GameReview>> PostGameReview(GameReview gameReview)
        {
            var displayName = User.FindFirst("displayname")?.Value ?? "Anonymous";

            gameReview.AuthorName = displayName;

            _context.GameReviews.Add(gameReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameReview", new { id = gameReview.ReviewId }, gameReview);
        }

        // DELETE: api/GameReview/5
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGameReview(int id)
        {
            var gameReview = await _context.GameReviews.FindAsync(id);
            if (gameReview == null)
            {
                return NotFound();
            }

            _context.GameReviews.Remove(gameReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameReviewExists(int id)
        {
            return _context.GameReviews.Any(e => e.ReviewId == id);
        }
    }
}
