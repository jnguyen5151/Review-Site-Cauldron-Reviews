using System.Text.Json.Serialization;

namespace ReviewAPI.DTOs.Steam
{
    public class GenreDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("description")]
        public string Genre { get; set; } = "";
    }
}
