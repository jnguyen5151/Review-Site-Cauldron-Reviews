namespace ReviewAPI.DTOs.Steam
{
    public class SteamAppDto
    {
        public int AppId { get; set; }
        public string Name { get; set; } = "";
        public decimal InitialPrice { get; set; }
        public string ReleaseDate { get; set; } = "";
        public string Owners { get; set; } = "";

        public string Developer { get; set; } = "";
        public string Publisher { get; set; } = "";
    }
}
