// -----------------------------------------------------------------------
// <copyright file="ReferService.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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

        public ReferViewModel? GetDiary(string id)
        {
            var diary = GetById(id);
            if (diary == null)
            {
                return null;
            }
            ReferViewModel viewModel = new ReferViewModel
            {
                Id = diary.id.ToString(),
                Title = diary.title,
                Content = diary.content,
            };

            return viewModel;
        }

        private Diary? GetById(string id)
        {
            var diaryList = _repository.Read(int.Parse(id));
            if (diaryList.Count != 1)
            {
                return null;
            }

            return diaryList.Single();
        }
    }
}