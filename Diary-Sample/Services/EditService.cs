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
    public class EditService : IEditService
    {
        private readonly ILogger<EditService> _logger;
        private readonly IDiaryRepository _repository;
        public EditService(ILogger<EditService> logger, IDiaryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public EditViewModel GetDiary(String id)
        {
            var diaryList = GetById(id);
            if ((diaryList == null) || (diaryList.Count == 0))
            {
                return null;
            }

            var diary = diaryList.First();
            EditViewModel viewModel = new EditViewModel();
            viewModel.Id = diary.id.ToString();
            viewModel.Title = diary.title;
            viewModel.Content = diary.content;

            return viewModel;
        }
        public bool UpdateDiary(EditViewModel editViewModel)
        {
            return UpdateById(editViewModel.Id, editViewModel.Title, editViewModel.Content);
        }
        public bool DeleteDiary(string id)
        {
            return DeleteById(id);
        }
        private List<Diary> GetById(string id)
        {
            var diary = _repository.read().Where(x => x.id == int.Parse(id)).ToList();
            return diary;
        }
        private bool UpdateById(string id, String title, String content)
        {
            Diary diary = new Diary() { id = int.Parse(id), title = title, content = content, update_date = new DateTime()};
            return _repository.Update(diary);
        }
        private bool DeleteById(string id)
        {
            return _repository.Delete(id);
        }

    }
}
