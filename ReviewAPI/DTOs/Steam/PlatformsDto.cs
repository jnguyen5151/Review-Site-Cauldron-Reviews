using System.Text.Json.Serialization;

namespace ReviewAPI.DTOs.Steam
{
    public class PlatformsDto
    {
        [JsonPropertyName("windows")]
        public bool Windows { get; set; }

        [JsonPropertyName("mac")]
        public bool Mac { get; set; }

        [JsonPropertyName("linux")]
        public bool Linux { get; set; }
    }
}
