using System.Text.Json.Serialization;

namespace ReviewAPI.DTOs.Steam
{
    public class ReleaseDateDto
    {
        [JsonPropertyName("coming_soon")]
        public bool ComingSoon { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; } = "";
    }
}
