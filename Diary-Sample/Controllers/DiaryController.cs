// -----------------------------------------------------------------------
// <copyright file="DiaryController.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Diagnostics;
using Diary_Sample.Models;
using Diary_Sample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            var model = _service.index();
            return View(model);
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