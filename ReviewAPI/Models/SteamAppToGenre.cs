using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class SteamAppToGenre
    {
        public int AppId { get; set; }
        [ForeignKey("AppId")]
        public SteamApp SteamApp { get; set; } = null!;

        public string GenreId { get; set; } = "";
        [ForeignKey("GenreId")]
        public SteamAppGenre SteamAppGenre { get; set; } = null!;
    }
}
