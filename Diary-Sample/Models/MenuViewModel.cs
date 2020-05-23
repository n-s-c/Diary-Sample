// -----------------------------------------------------------------------
// <copyright file="MenuViewModel.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Diary_Sample.Models
{
    public class MenuViewModel
    {
        // 日記一覧（1ページ分）
        public List<DiaryRow> DiaryList { get; } = new List<DiaryRow>();

        // 全件数
        public int TotalNumber { get; set; } = 0;
        // 現在のページ
        public int NowPage { get; set; } = 1;
        // 全ページ数
        public int TotalPageNumber { get; set; } = 1;
        // 通知
        public string Notification { get; set; } = string.Empty;
    }
    public readonly struct DiaryRow : IEquatable<DiaryRow>
    {
        public string No { get; }
        public string Title { get; }
        public string PostDate { get; }
        public DiaryRow(int id, string title, DateTime post_date)
        {
            CultureInfo cultureJp = CultureInfo.CreateSpecificCulture("ja-JP");

            No = id.ToString("D", cultureJp);
            Title = title;
            PostDate = post_date.ToString("yyyy/MM/dd", cultureJp);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(DiaryRow other)
        {
            return No == other.No && Title == other.Title && PostDate == other.PostDate;
        }

        public static bool operator ==(DiaryRow left, DiaryRow right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DiaryRow left, DiaryRow right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is DiaryRow row && Equals(row);
        }
    }
}