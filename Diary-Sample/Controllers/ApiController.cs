// -----------------------------------------------------------------------
// <copyright file="ApiController.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using Diary_Sample.Models;
using Diary_Sample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Controllers
{
    [ApiController]
    [Route("api/v1/[action]")]
    [Produces("application/json")]
    public class ApiController : ControllerBase, IApiController
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IApiService _service;
        public ApiController(ILogger<ApiController> logger, IApiService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{page}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        public ActionResult<List<DiaryRow>> Lists(int page) => page > 0 ? (ActionResult)Ok(_service.Lists(page)) : BadRequest();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> Counts() => Ok(_service.Counts());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<DiaryModel> Diary(int id) => Ok(_service.Diary(id));
    }
}