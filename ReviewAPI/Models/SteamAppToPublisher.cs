using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class SteamAppToPublisher
    {
        public int AppId { get; set; }
        [ForeignKey("AppId")]
        public SteamApp SteamApp { get; set; } = null!;
        
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public SteamAppPublisher SteamAppPublisher { get; set; } = null!;
    }
}
