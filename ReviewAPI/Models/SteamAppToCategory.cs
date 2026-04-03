using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class SteamAppToCategory
    {
        public int AppId { get; set; }
        [ForeignKey("AppId")]
        public SteamApp SteamApp { get; set; } = null!;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public SteamAppCategory SteamAppCategory { get; set; } = null!;
    }
}
