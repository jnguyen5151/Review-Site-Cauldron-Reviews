using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class SteamApp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AppId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = "";

        public string Type { get; set; } = "";

        public decimal Price { get; set; }
        public int OwnersMax { get; set; }
        public int OwnersMin { get; set; }
        public int OwnersAvg { get; set; }
        public int RequiredAge { get; set; }

        public string? Description { get; set; }
        public string? ReleaseDate { get; set; }
        public string? HeaderImage { get; set; }
        
        public bool IsFree { get; set; }
        public bool Windows { get; set; }
        public bool Mac { get; set; }
        public bool Linux { get; set; }

        public bool IsEnriched { get; set; } = false;

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public ICollection<SteamAppToGenre> SteamAppGenre { get; set; } = new List<SteamAppToGenre>();
        public ICollection<SteamAppToDeveloper> SteamAppDeveloper { get; set; } = new List<SteamAppToDeveloper>();
        public ICollection<SteamAppToPublisher> SteamAppPublisher { get; set; } = new List<SteamAppToPublisher>();
        public ICollection<SteamAppToCategory> SteamAppCategory { get; set; } = new List<SteamAppToCategory>();
    }
}
