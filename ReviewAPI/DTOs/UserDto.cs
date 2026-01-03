namespace ReviewAPI.DTOs
{
    public class UserDto
    {
        public string id { get; set; } = null!;
        public string displayName { get; set; } = null!;
        public IList<string> roles { get; set; } = [];
        public string userName { get; set; } = null!;
        public string email { get; set; } = null!;
    }
}
