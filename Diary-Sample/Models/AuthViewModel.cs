// -----------------------------------------------------------------------
// <copyright file="AuthViewModel.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace Diary_Sample.Models
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "Eメールは必須です。")]
        [EmailAddress]
        [Display(Name = "Eメール")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "パスワードは必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string Notification { get; set; } = string.Empty;
    }
}