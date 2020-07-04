// -----------------------------------------------------------------------
// <copyright file="DiaryRepository.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Diary_Sample.Entities;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

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

        public bool create(Diary diary)
        {
            try
            {
                _context.Add(diary);
                return _context.SaveChanges() > 0;
            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
            }
            return false;
        }
        public List<Diary> read(int page, int count)
        {
            List<Diary> result = new List<Diary>();
            try
            {
                result = _context.diary.OrderByDescending(x => x.post_date).Skip((page - 1) * count).Take(count).ToList();
                result.ForEach(x =>
               {
                   _logger.LogInformation($"{x.id} {x.title} {x.content} {x.post_date} {x.update_date}");
               });
            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
            }

            return result;
        }

        public int readCount()
        {
            int count = 0;
            try
            {
                count = _context.diary.Count();
            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
            }

            return count;
        }

        public List<Diary> Read(int id)
        {
            List<Diary> result = new List<Diary>();
            try
            {
                result = _context.diary.Where(x => x.id == id).ToList();
            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
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
                record.update_date = diary.update_date;
                _context.SaveChanges();

            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
            return true;

        }

        public bool Delete(string id)
        {
            try
            {
                var record = _context.diary.ToList().Single(x => x.id == int.Parse(id));
                _context.diary.Remove(record);
                _context.SaveChanges();

            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
            return true;
        }
    }
}