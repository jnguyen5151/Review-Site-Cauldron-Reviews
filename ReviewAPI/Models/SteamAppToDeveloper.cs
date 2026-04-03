using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class SteamAppToDeveloper
    {
        public int AppId { get; set; }
        [ForeignKey("AppId")]
        public SteamApp SteamApp { get; set; } = null!;

        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public SteamAppDeveloper SteamAppDeveloper { get; set; } = null!;
    }
}
