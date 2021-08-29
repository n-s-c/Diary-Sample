// -----------------------------------------------------------------------
// <copyright file="SharedService.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using Diary_Sample.Models;
using Diary_Sample.Repositories;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Services
{
    public class SharedService : ISharedService
    {
        private readonly ILogger<ISharedService> _logger;
        private readonly IDiaryRepository _repository;

        public SharedService(ILogger<ISharedService> logger, IDiaryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public List<DiaryRow> Lists(int page, int count) =>
                _repository.read(page, count)
                .Select(diary => new DiaryRow(diary.id, diary.title, diary.post_date))
                .ToList();
        public int Counts() => _repository.readCount();

        public bool Diary(int id, ref DiaryModel? diary)
        {
            var diaryList = _repository.Read(id).Select(diary => new DiaryModel(diary.id, diary.title, diary.content)).ToList();
            if (diaryList.Count != 1)
            {
                return false;
            }

            diary = diaryList[0];

            return true;
        }
    }
}