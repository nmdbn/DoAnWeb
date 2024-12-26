namespace DoAnWeb.Models
{
    public class Manga
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();
    }
}
