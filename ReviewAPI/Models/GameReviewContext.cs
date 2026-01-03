using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReviewAPI.Models
{
    public class GameReviewContext : IdentityDbContext<Users> 
    {
        public GameReviewContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<GameReview> GameReviews { get; set; }
    }
}
