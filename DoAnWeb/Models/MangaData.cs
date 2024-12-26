namespace DoAnWeb.Models
{
    public class MangaData
    {
        public string Id { get; set; }
        public MangaAttributes Attributes { get; set; }
        public List<Relationship> Relationships { get; set; }
    }
}
