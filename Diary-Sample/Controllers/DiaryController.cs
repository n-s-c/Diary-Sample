using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diary_Sample.Models;
using Diary_Sample.Services;

namespace Diary_Sample.Controllers
{
    public class DiaryController : Controller
    {
        private readonly ILogger<DiaryController> _logger;
        private readonly IDiaryService _service;
        public DiaryController(ILogger<DiaryController> logger, IDiaryService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            _service.index();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}