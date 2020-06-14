using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary_Sample.Entities;
using Diary_Sample.Models;

namespace Diary_Sample.Services
{
    public interface IReferService
    {
        public ReferViewModel GetDiary(String id);
    }
}
