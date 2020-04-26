using System;
using System.Collections.Generic;

namespace Diary_Sample.Models
{
    public class IndexViewModel
    {
        public List<DiaryRow> diaryList { get; set; } = new List<DiaryRow>();
    }
    public struct DiaryRow
    {
        public string no { get; }
        public string title { get; }
        public string postDate { get; }

        public DiaryRow(int id, string title, DateTime post_date)
        {
            this.no = id.ToString();
            this.title = title;
            this.postDate = post_date.ToString();
        }
    }
}