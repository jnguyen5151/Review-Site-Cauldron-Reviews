using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Models
{
    public class SteamAppPublisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        public string Publisher { get; set; } = "";

        public ICollection<SteamAppToPublisher> GamePublisher { get; set; } = new List<SteamAppToPublisher>();
    }
}
