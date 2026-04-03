using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewAPI.Models
{
    public class SteamAppCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Category { get; set; } = "";

        public ICollection<SteamAppToCategory> GameCategorys { get; set; } = new List<SteamAppToCategory>();
    }
}
