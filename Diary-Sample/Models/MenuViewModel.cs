// -----------------------------------------------------------------------
// <copyright file="MenuViewModel.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Diary_Sample.Models
{
    public class MenuViewModel
    {
        public const int PageCount = 5;

        public MenuViewModel(List<DiaryRow> diaryList, int totalNumber, int nowPage, string notification)
        {
            DiaryList = diaryList;
            TotalNumber = totalNumber;
            NowPage = nowPage;

            // 全件数からページ数を求める
            TotalPageNumber = TotalNumber % PageCount == 0
                    ? TotalNumber / PageCount : (TotalNumber / PageCount) + 1;

            Notification = string.IsNullOrEmpty(notification) ? string.Empty : notification;

        }
        // 日記一覧（1ページ分）
        public List<DiaryRow> DiaryList { get; } = new List<DiaryRow>();
        // 全件数
        public int TotalNumber { get; set; } = 0;
        // 現在のページ
        public int NowPage { get; set; } = 1;
        // 全ページ数
        public int TotalPageNumber { get; } = 1;
        // 通知
        public string Notification { get; set; } = string.Empty;
    }

    public readonly struct DiaryRow : IEquatable<DiaryRow>
    {
        public string Id { get; }
        public string Title { get; }
        public string PostDate { get; }
        public DiaryRow(int id, string title, DateTime post_date)
        {
            CultureInfo cultureJp = CultureInfo.CreateSpecificCulture("ja-JP");

            Id = id.ToString("D", cultureJp);
            Title = title;
            PostDate = post_date.ToString("yyyy/MM/dd", cultureJp);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(DiaryRow other)
        {
            return Id == other.Id && Title == other.Title && PostDate == other.PostDate;
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