using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Models
{
    public class IngestionState
    {
        [Key]
        public int Id { get; set; }

        public int LastAppId { get; set; }
    }
}
