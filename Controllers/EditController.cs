// -----------------------------------------------------------------------
// <copyright file="EditController.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using Diary_Sample.Common;
using Diary_Sample.Models;
using Diary_Sample.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diary_Sample.Controllers
{
    public class EditController : Controller
    {
        private readonly IEditService _service;

        private const string ErrorPath = "Views/Shared/Error.cshtml";

        private const string DeleteOkMessage = "削除しました。";

        public EditController(IEditService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            if (!CommonUtil.CheckId(id))
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
        public IActionResult Edit(EditViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var ret = _service.UpdateDiary(editViewModel);
            if (!ret)
            {
                return View(ErrorPath, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return RedirectToAction("Index", "Refer", new {id = editViewModel.Id });

        }

        [HttpGet]
        public IActionResult Delete(string id)
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

            TempData["notification"] = DeleteOkMessage;
            return RedirectToAction("Index", "Menu");
        }
    }
}
