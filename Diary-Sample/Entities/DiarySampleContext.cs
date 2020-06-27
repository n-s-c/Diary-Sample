// -----------------------------------------------------------------------
// <copyright file="DiarySampleContext.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Entities
{
    public partial class DiarySampleContext : DbContext
    {
        public DbSet<Diary>? diary { get; set; }
        public static readonly ILoggerFactory MySQLLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new System.Exception();
            }
            if (!optionsBuilder.IsConfigured)
            {
                // TODO DB設定は後で設定ファイルに持つようにする・環境ごとに設定を変更できるようにする
                optionsBuilder
                .UseLoggerFactory(MySQLLoggerFactory)
                .UseMySQL(@"server=localhost;port=3306;database=DiarySample;userid=user;pwd=password;sslmode=Required;");
            }
        }
    }
}