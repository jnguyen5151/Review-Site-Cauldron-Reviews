namespace ReviewAPI.DTOs
{
    public class ReviewCardDto
    {
        public string authorName { get; set; } = null!;
        public int reviewId { get; set; }
        public string gameName { get; set; } = null!;
        public int rating { get; set; }
        public DateTime createdAt { get; set; }
        public string title { get; set; } = null!;
        public uint likes { get; set; } = 0;
        public uint dislikes { get; set; } = 0;
        public uint commentNumber { get; set; } = 0;
    }
}
