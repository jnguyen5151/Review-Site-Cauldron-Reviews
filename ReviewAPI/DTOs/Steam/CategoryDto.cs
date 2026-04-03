using System.Text.Json.Serialization;

namespace ReviewAPI.DTOs.Steam
{
    public class CategoryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Category { get; set; } = "";
    }
}
