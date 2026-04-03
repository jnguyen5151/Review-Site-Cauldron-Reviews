using System.Text.Json.Serialization;

namespace ReviewAPI.DTOs.Steam
{
    public class SteamStoreDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public SteamStoreDataDto Data { get; set; } = new SteamStoreDataDto();
        
    }
}
