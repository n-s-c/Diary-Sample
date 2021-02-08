// -----------------------------------------------------------------------
// <copyright file="EmailSenderLocal.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Infra.Mail
{
    public class EmailSenderLocal : IEmailSender
    {
        private readonly ILogger<EmailSenderLocal> _logger;
        public EmailSenderLocal(ILogger<EmailSenderLocal> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // ローカルではログに出力する
            return Task.Run(() =>
            {
                _logger.LogInformation($"{email}");
                _logger.LogInformation($"{htmlMessage}");
            });
        }
    }
}