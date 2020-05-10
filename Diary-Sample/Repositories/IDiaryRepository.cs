// -----------------------------------------------------------------------
// <copyright file="IDiaryRepository.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using Diary_Sample.Entities;

namespace Diary_Sample.Repositories
{
    public interface IDiaryRepository
    {
        public List<Diary> read();
    }
}