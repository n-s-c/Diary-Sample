// -----------------------------------------------------------------------
// <copyright file="ApiController.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Diary_Sample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Controllers
{
    [ApiController]
    [Route("api/v1/[action]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IApiService _service;
        public ApiController(ILogger<ApiController> logger, IApiService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        [Route("{page}")]
        public IActionResult Lists(int page) => page > 0 ? (IActionResult)Ok(_service.Lists(page)) : BadRequest();
        [HttpGet]
        public IActionResult Counts() => Ok(_service.Counts());
    }
}