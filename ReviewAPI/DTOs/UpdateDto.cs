using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.DTOs
{
    public class UpdateDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [StringLength(30, ErrorMessage = "Description cannot exceed 30 characters.")]
        public string? DisplayName { get; set; }

        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string? Description { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(30, ErrorMessage = "Description cannot exceed 30 characters.")]
        public string? Pronouns { get; set; }

        public Boolean SafeMode { get; set; }

    }
}
