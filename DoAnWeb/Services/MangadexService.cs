using DoAnWeb.Models;
using Newtonsoft.Json;

namespace DoAnWeb.Services
{
    //public class MangadexService
    //{
    //    private readonly HttpClient _httpClient;
    //    private const string BaseUrl = "https://api.mangadex.org";

    //    public MangadexService()
    //    {
    //        _httpClient = new HttpClient();
    //    }

    //    public async Task<List<Manga>> GetMangaList(int limit = 10)
    //    {
    //        var response = await _httpClient.GetAsync($"{BaseUrl}/manga?limit={limit}&includes[]=cover_art");
    //        var content = await response.Content.ReadAsStringAsync();
    //        // Parse JSON response and map to Manga objects
    //        // This is simplified - you'll need to handle the actual Mangadex API response format
    //        return JsonConvert.DeserializeObject<List<Manga>>(content);
    //    }

    //    public async Task<List<string>> GetChapterImages(string chapterId)
    //    {
    //        var response = await _httpClient.GetAsync($"{BaseUrl}/at-home/server/{chapterId}");
    //        var content = await response.Content.ReadAsStringAsync();
    //        // Parse JSON response to get image URLs
    //        // This is simplified - you'll need to handle the actual Mangadex API response format
    //        return JsonConvert.DeserializeObject<List<string>>(content);
    //    }
    //}

    public class MangadexService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.mangadex.org";

        public MangadexService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Manga>> GetMangaList(int limit = 10)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/manga?limit={limit}&includes[]=cover_art&contentRating[]=safe");
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine("API Response: " + content);

            // Đọc response từ Mangadex API
            var jsonResponse = JsonConvert.DeserializeObject<MangadexResponse>(content);

            var mangas = new List<Manga>();

            foreach (var data in jsonResponse.Data)
            {
                var manga = new Manga
                {
                    Id = data.Id,
                    Title = data.Attributes.Title.ContainsKey("en")
                        ? data.Attributes.Title["en"]
                        : data.Attributes.Title.FirstOrDefault().Value,
                    Description = data.Attributes.Description.ContainsKey("en")
                        ? data.Attributes.Description["en"]
                        : data.Attributes.Description.FirstOrDefault().Value,
                    CoverUrl = GetCoverUrl(data)
                };
                mangas.Add(manga);
            }

            return mangas;
        }

        private string GetCoverUrl(MangaData data)
        {
            var coverArt = data.Relationships
                .FirstOrDefault(r => r.Type == "cover_art");

            if (coverArt != null)
            {
                return $"https://uploads.mangadex.org/covers/{data.Id}/{coverArt.Attributes?.FileName}";
            }

            return string.Empty;
        }

        public async Task<List<string>> GetChapterImages(string chapterId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/at-home/server/{chapterId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ChapterResponse>(content);

            var baseUrl = result.BaseUrl;
            var hash = result.Chapter.Hash;
            var imageUrls = result.Chapter.Data.Select(fileName =>
                $"{baseUrl}/data/{hash}/{fileName}").ToList();

            return imageUrls;
        }
    }
}
