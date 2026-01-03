using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class GameReview
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AuthorName { get; set; } = "";

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string GameName { get; set; } = "";

        [Range(0, 100, ErrorMessage = "Rating must be between 1 - 100")]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = "";

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = "";

        public uint Likes { get; set; }

        public uint Dislikes { get; set; }

        public uint CommentNumber { get; set; }
    }


}
