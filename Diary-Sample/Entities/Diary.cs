using System;

namespace Diary_Sample.Entities
{
    public class Diary
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime post_date { get; set; }
        public DateTime update_date { get; set; }
    }
}