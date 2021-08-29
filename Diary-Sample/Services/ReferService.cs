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
        private readonly ISharedService _service;

        public ReferService(IDiaryRepository repository, ISharedService service)
        {
            _repository = repository;
            _service = service;
        }

        public ReferViewModel? GetDiary(int id)
        {

            DiaryModel? diary = null;
            if (!_service.Diary(id, ref diary))
            {
                return null;
            }

            ReferViewModel? viewModel = new ReferViewModel
            {
                Id = diary.Id,
                Title = diary.Title,
                Content = diary.Content,
            };

            return viewModel;
        }
    }
}