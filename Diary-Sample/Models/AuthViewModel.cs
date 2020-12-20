using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Models
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "Eメールは必須です。")]
        [EmailAddress]
        [Display(Name = "Eメール")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "パスワードは必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; } = String.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string Notification { get; set; } = string.Empty;
    }
}