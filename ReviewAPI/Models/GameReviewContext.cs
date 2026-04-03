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

        public DbSet<SteamApp> SteamApps { get; set; }

        public DbSet<SteamAppGenre> Genres { get; set; }
        public DbSet<SteamAppToGenre> AppGenres { get; set; }

        public DbSet<SteamAppDeveloper> Developers { get; set; }
        public DbSet<SteamAppToDeveloper> AppDevelopers { get; set; }

        public DbSet<SteamAppPublisher> Publishers { get; set; }
        public DbSet<SteamAppToPublisher> AppPublishers { get; set; }

        public DbSet<SteamAppCategory> Categories { get; set; }
        public DbSet<SteamAppToCategory> AppCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SteamAppToGenre>()
                .HasKey(x => new { x.AppId, x.GenreId });

            builder.Entity<SteamAppToDeveloper>()
                .HasKey(x => new { x.AppId, x.DeveloperId });

            builder.Entity<SteamAppToPublisher>()
                .HasKey(x => new { x.AppId, x.PublisherId });

            builder.Entity<SteamAppToCategory>()
                .HasKey(x => new { x.AppId, x.CategoryId });

            builder.Entity<SteamApp>()
                .Property(a => a.Price)
                .HasPrecision(10, 2);
        }
    }
}
