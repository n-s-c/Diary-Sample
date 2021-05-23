using System.ComponentModel.DataAnnotations;

namespace Diary_Sample.Models
{
    public class UserAdminAccountDeleteViewModel
    {
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "パスワードは必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; } = string.Empty;

        public string Notification { get; set; } = string.Empty;

        public bool UpdateResult { get; set; } = true;

    }

}