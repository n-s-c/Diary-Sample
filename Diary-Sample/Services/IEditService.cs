using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary_Sample.Entities;
using Diary_Sample.Models;

namespace Diary_Sample.Services
{
    public interface IEditService
    {
        public EditViewModel GetDiary(String id);
        public bool UpdateDiary(EditViewModel editViewModel);
        public bool DeleteDiary(String id);
    }
}
