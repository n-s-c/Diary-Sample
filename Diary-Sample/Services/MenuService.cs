// -----------------------------------------------------------------------
// <copyright file="MenuService.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Diary_Sample.Entities;
using Diary_Sample.Models;
using Diary_Sample.Repositories;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Services
{
    public class MenuService : IMenuService
    {
        private const int InitialPage = 1;
        private const int PageCount = 5;
        private readonly ILogger<MenuService> _logger;
        private readonly IDiaryRepository _repository;

        public MenuService(ILogger<MenuService> logger, IDiaryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public MenuViewModel Index()
        {
            return createModel(InitialPage);
        }

        public MenuViewModel Paging(int page)
        {
            return createModel(page);
        }

        private MenuViewModel createModel(int page)
        {
            MenuViewModel model = new MenuViewModel();

            // 一覧データ設定
            _repository.read(page, PageCount).ForEach(x =>
                 {
                     model.DiaryList.Add(new DiaryRow(x.id, x.title, x.post_date));
                 });

            // 全件数設定
            model.totalNumber = _repository.readCount();

            // 現在のページ設定
            model.NowPage = page;

            // 全ページ数設定
            model.TotalPageNumber =
                model.totalNumber % PageCount == 0
                    ? model.totalNumber / PageCount : (model.totalNumber / PageCount) + 1;

            return model;
        }
    }
}