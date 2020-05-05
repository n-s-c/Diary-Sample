// -----------------------------------------------------------------------
// <copyright file="DiaryService.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Diary_Sample.Models;
using Diary_Sample.Repositories;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly ILogger<DiaryService> _logger;
        private readonly IDiaryRepository _repository;
        public DiaryService(ILogger<DiaryService> logger, IDiaryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IndexViewModel index()
        {
            IndexViewModel model = new IndexViewModel();

            _repository.read().ForEach(x =>
            {
                model.diaryList.Add(new DiaryRow(x.id, x.title, x.post_date));
            });
            return model;
        }
    }
}