// -----------------------------------------------------------------------
// <copyright file="ISharedService.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using Diary_Sample.Models;

namespace Diary_Sample.Services
{
    public interface ISharedService
    {
        public List<DiaryRow> Lists(int page, int count);
        public int Counts();
    }
}