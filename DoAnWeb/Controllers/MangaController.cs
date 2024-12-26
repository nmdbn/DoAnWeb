using DoAnWeb.Data;
using DoAnWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAnWeb.Controllers
{
    public class MangaController : Controller
    {
        private readonly MangaContext _context;
        private readonly MangadexService _mangadexService;

        public MangaController(MangaContext context, MangadexService mangadexService)
        {
            _context = context;
            _mangadexService = mangadexService;
        }

        public async Task<IActionResult> Index()
        {
            var mangas = await _mangadexService.GetMangaList();
            return View(mangas);
        }

        public async Task<IActionResult> ReadChapter(string chapterId)
        {
            var images = await _mangadexService.GetChapterImages(chapterId);
            return View(images);
        }
    }
}
