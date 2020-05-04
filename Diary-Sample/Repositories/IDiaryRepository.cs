using Diary_Sample.Entities;
using System.Collections.Generic;

namespace Diary_Sample.Repositories
{
    public interface IDiaryRepository
    {
        public List<Diary> read();
    }
}