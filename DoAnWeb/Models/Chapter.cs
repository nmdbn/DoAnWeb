namespace DoAnWeb.Models
{
    public class Chapter
    {
        public string Id { get; set; }
        public string MangaId { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public Manga Manga { get; set; }
    }
}
