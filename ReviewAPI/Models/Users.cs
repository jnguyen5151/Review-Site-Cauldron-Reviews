using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Models
{
    public class Users : IdentityUser
    {
        [StringLength(30, ErrorMessage = "Description cannot exceed 30 characters.")]
        public string DisplayName { get; set; } = "Anonymous";
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string Description { get; set; } = ""; 

        public ICollection<GameReview> Reviews { get; set; } = [];

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public Boolean SafeMode { get; set; } = true;

        public DateTime? Birthday { get; set; }

        [StringLength(30, ErrorMessage = "Description cannot exceed 30 characters.")]
        public string? Pronouns { get; set; } 
    }
}
