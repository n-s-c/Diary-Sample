using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Sample.Models
{
    public class UserAdminEmailViewModel
    {
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Eメールは必須です。")]
        [EmailAddress]
        [Display(Name = "Eメール")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "変更後Eメールは必須です。")]
        [EmailAddress]
        [Display(Name = "変更後Eメール")]
        public string NewEmail {get; set; } = string.Empty;

        public string Notification { get; set; } = string.Empty;

        public bool UpdateResult { get; set; } = true;

    }
}
