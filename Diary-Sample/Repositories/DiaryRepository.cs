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
        public List<Diary> read()
        {
            List<Diary> result = new List<Diary>();
            try
            {
                result = _context.diary.ToList();
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

    }
}