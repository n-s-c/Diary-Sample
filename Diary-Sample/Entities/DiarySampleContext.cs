// -----------------------------------------------------------------------
// <copyright file="DiarySampleContext.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace Diary_Sample.Entities
{
    public partial class DiarySampleContext : DbContext
    {
        public DbSet<Diary> diary { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO DB設定は後で設定ファイルに持つようにする・環境ごとに設定を変更できるようにする
                optionsBuilder.UseMySQL(@"server=localhost;port=3306;database=DiarySample;userid=user;pwd=password;sslmode=Required;");
            }
        }
    }
}
