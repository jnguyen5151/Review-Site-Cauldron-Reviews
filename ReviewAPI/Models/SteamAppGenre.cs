using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class SteamAppGenre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string GenreId { get; set; }

        [Required]
        public string Genre { get; set; } = "";

        public ICollection<SteamAppToGenre> GameGenres { get; set; } = new List<SteamAppToGenre>();
    }
}
