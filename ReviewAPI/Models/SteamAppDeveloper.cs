using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Models
{
    public class SteamAppDeveloper
    {
        [Key]
        public int DeveloperId { get; set; }

        [Required]
        public string Developer { get; set; } = "";

        public ICollection<SteamAppToDeveloper> GameDevelopers { get; set; } = new List<SteamAppToDeveloper>();
    }
}
