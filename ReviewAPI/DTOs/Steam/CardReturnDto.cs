namespace ReviewAPI.DTOs.Steam
{
    public class CardReturnDto
    {
        public int AppId { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public decimal Price { get; set; }
        public int RequiredAge { get; set; }
        public bool IsFree { get; set; }
        public string? ReleaseDate { get; set; }
        public string? HeaderImage { get; set; }
        public string? Description { get; set; }
        
    }
}
