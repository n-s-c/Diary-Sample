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
        private const int PageCount = 5;
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
        private static int createNo(int index, int page)
        {
            // indexは0から始まるので1加算する
            index++;

            // pageは1から始まるので1減算する
            page--;

            // 日記一覧のNoを計算返却する
            return index + (page * PageCount);
        }
        private MenuViewModel createModel(int page, string notification)
        {
            MenuViewModel model = new MenuViewModel();

            // 一覧データ設定
            _repository.read(page, PageCount).Select((diary, index) => new { diary, index }).ToList().ForEach(x =>
                 {
                     model.DiaryList.Add(new DiaryRow(createNo(x.index, page), x.diary.title, x.diary.post_date));
                 });

            // 全件数設定
            model.TotalNumber = _repository.readCount();

            // 現在のページ設定
            model.NowPage = page;

            // 全ページ数設定
            model.TotalPageNumber =
                model.TotalNumber % PageCount == 0
                    ? model.TotalNumber / PageCount : (model.TotalNumber / PageCount) + 1;

            if (!string.IsNullOrEmpty(notification))
            {
                model.Notification = notification;
            }
            return model;
        }
    }
}