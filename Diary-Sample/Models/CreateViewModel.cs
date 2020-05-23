// -----------------------------------------------------------------------
// <copyright file="CreateViewModel.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Diary_Sample.Models
{
    public class CreateViewModel
    {
        // タイトル
        public string Title { get; set; } = string.Empty;
        // 本文
        public string Content { get; set; } = string.Empty;
        // 通知
        public string Notification { get; set; } = string.Empty;
    }
}