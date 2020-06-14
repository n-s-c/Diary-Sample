using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Diary_Sample.Entities;
using System.Linq;
using System.Reflection;

namespace Diary_Sample.Repositories
{
    public class DiaryRepository : IDiaryRepository
    {
        private readonly ILogger<DiaryRepository> _logger;
        private readonly DiarySampleContext _context;
        public DiaryRepository(ILogger<DiaryRepository> logger, DiarySampleContext context)
        {
            _logger = logger;
            _context = context;
        }
        public List<Diary> read()
        {
            List<Diary> result = new List<Diary>();
            try
            {
                result = _context.diary.ToList();
                result.ForEach(x =>
               {
                   Console.WriteLine($"{x.id} {x.title} {x.content} {x.post_date} {x.update_date}");
               });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public bool Update(Diary diary)
        {
            try
            {
                var record = _context.diary.ToList().Single(x => x.id == diary.id);
                record.title = diary.title;
                record.content = diary.content;
                _context.SaveChanges();
 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;

        }

        public bool Delete(string id)
        {
            try
            {
                var record = _context.diary.ToList().Single(x => x.id == int.Parse(id));
                _context.diary.ToList().Remove(record);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
