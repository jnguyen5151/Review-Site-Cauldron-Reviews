using System.Text.Json.Serialization;

namespace ReviewAPI.DTOs.Steam
{
    public class SteamStoreDataDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";

        [JsonPropertyName("required_age")]
        public int RequiredAge { get; set; }

        [JsonPropertyName("is_free")]
        public bool IsFree { get; set; }

        [JsonPropertyName("short_description")]
        public string? ShortDescription { get; set; }

        [JsonPropertyName("header_image")]
        public string? CardImage { get; set; }

        [JsonPropertyName("platforms")]
        public PlatformsDto? Platforms { get; set; }

        [JsonPropertyName("categories")]
        public List<CategoryDto> Category { get; set; } = new List<CategoryDto>();

        [JsonPropertyName("genres")]
        public List<GenreDto> Genre { get; set; } = new List<GenreDto>();

        [JsonPropertyName("release_date")]
        public ReleaseDateDto? ReleaseDate { get; set; }

    }
}
