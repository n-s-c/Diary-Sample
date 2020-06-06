// -----------------------------------------------------------------------
// <copyright file="MenuService.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using Diary_Sample.Models;
using Diary_Sample.Repositories;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Services
{
    public class MenuService : IMenuService
    {
        private const int InitialPage = 1;
        private readonly ILogger<MenuService> _logger;
        private readonly IDiaryRepository _repository;

        public MenuService(ILogger<MenuService> logger, IDiaryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public MenuViewModel Index(string notification)
        {
            return createModel(InitialPage, notification);
        }

        public MenuViewModel Paging(int page)
        {
            return createModel(page, string.Empty);
        }

        private MenuViewModel createModel(int page, string notification)
        {
            return new MenuViewModel(
                _repository.read(page, MenuViewModel.PageCount)
                .Select(diary => new DiaryRow(diary.id, diary.title, diary.post_date))
                .ToList(),
                _repository.readCount(),
                page,
                notification);
        }
    }
}