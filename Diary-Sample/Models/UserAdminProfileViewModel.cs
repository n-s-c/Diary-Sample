using System.ComponentModel.DataAnnotations;

namespace Diary_Sample.Models
{
    public class UserAdminProfileViewModel
    {
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "ユーザ名は必須です。")]
        [Display(Name = "ユーザ名")]
        public string UserName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "電話番号が不正です。")]
        [Display(Name = "電話番号")]
        public string? PhoneNumber { get; set; } = string.Empty;

        public string Notification { get; set; } = string.Empty;

        public bool UpdateResult { get; set; } = true;

    }
}