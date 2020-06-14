using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Extensions.Logging;
using Diary_Sample.Models;
using Diary_Sample.Services;
using System.Reflection;

namespace Diary_Sample.Controllers
{
    public class EditController : Controller
    {
        private readonly ILogger<EditController> _logger;

        private readonly IEditService _service;

        private readonly string ErrorPath = "Views/Shared/Error.cshtml";

        private readonly string ReferIndexPath = "/Refer/Index/";

        private readonly string DiaryIndexPath = "/Diary/Index/";

        public EditController(ILogger<EditController> logger, IEditService service)
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

        [HttpPost]
        public IActionResult Edit(String id, EditViewModel editViewModel)
        {
            if (id == null)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            editViewModel.Id = id;
            
            var ret = _service.UpdateDiary(editViewModel);
            if (!ret)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return Redirect(ReferIndexPath + id);
        }

        public IActionResult Delete(String id)
        {
            if (id == null)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var ret = _service.DeleteDiary(id);
            if (!ret)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return Redirect(DiaryIndexPath + id);

        }
    }
}
