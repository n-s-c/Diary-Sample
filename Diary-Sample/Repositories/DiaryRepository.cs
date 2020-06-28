// -----------------------------------------------------------------------
// <copyright file="DiaryRepository.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
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
                _logger.LogError(e.StackTrace);
                throw;
            }
        }
        public List<Diary> read(int page, int count)
        {
            try
            {
                return _context.diary
                    .OrderByDescending(x => x.post_date)
                    .Skip((page - 1) * count)
                    .Take(count)
                    .ToList()
                    .Select(x =>
                    {
                        outputLog(x);
                        return x;
                    })
                    .ToList();
            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                throw;
            }
        }

        public int readCount()
        {
            try
            {
                return _context.diary.Count();
            }
            catch (MySqlException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                throw;
            }
        }

        private void outputLog(Diary x)
        {
            // DBから取得した内容をログに出力
            _logger.LogInformation($"{x.id} {x.title} {x.content} {x.post_date} {x.update_date}");
        }
    }
}