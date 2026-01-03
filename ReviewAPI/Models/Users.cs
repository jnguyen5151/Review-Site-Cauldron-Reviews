using Microsoft.AspNetCore.Identity;

namespace ReviewAPI.Models
{
    public class Users : IdentityUser
    {
        public string DisplayName { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<GameReview> Reviews { get; set; } = [];

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
