// -----------------------------------------------------------------------
// <copyright file="ReferService.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Diary_Sample.Entities;
using Diary_Sample.Models;
using Diary_Sample.Repositories;

namespace Diary_Sample.Services
{
    public class ReferService : IReferService
    {
        private readonly IDiaryRepository _repository;

        public ReferService(IDiaryRepository repository)
        {
            _repository = repository;
        }

        public ReferViewModel? GetDiary(int id)
        {
            var diaryList = GetById(id);
            if (diaryList.Count == 0)
            {
                return null;
            }

            var diary = diaryList.First();
            ReferViewModel? viewModel = new ReferViewModel
            {
                Id = diary.id.ToString(),
                Title = diary.title,
                Content = diary.content,
            };

            return viewModel;
        }

        private List<Diary> GetById(int id)
        {
            var diaryList = _repository.Read(id);
            if (diaryList.Count != 1)
            {
                return new List<Diary>();
            }

            return diaryList;
        }
    }
}