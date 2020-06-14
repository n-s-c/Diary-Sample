using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Diary_Sample.Entities;
using Diary_Sample.Repositories;
using Diary_Sample.Models;

namespace Diary_Sample.Services
{
    public class ReferService : IReferService
    {
        private readonly ILogger<ReferService> _logger;
        private readonly IDiaryRepository _repository;
        public ReferService(ILogger<ReferService> logger, IDiaryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public ReferViewModel GetDiary(String id)
        {
            var diary = GetById(id);
            ReferViewModel viewModel = new ReferViewModel();
            viewModel.Id = diary.id.ToString();
            viewModel.Title = diary.title;
            viewModel.Content = diary.content;

            return viewModel;
        }

        private Diary GetById(string id)
        {
            var diary = _repository.read().Single(x => x.id == int.Parse(id));
            return diary;
        }
    }
}
