using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diary_Sample.Models;
using Diary_Sample.Entities;
using Diary_Sample.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

namespace Diary_Sample.Controllers
{
    public class ReferController : Controller
    {
        private readonly ILogger<ReferController> _logger;

        private readonly IReferService _service;

        private readonly string ErrorPath = "Views/Shared/Error.cshtml";
        public ReferController(ILogger<ReferController> logger, IReferService service)
        {
            _logger = logger;
            _service = service;
        }
        public IActionResult Index(String id)
        {
            if (id == null)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var viewModel = _service.GetDiary(id);
            if (viewModel == null)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return View(viewModel);
        }
    }
}
