using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diary_Sample.Models
{
    public class UserAdminViewModel
    {
        [Required(ErrorMessage = "ユーザIDは必須です。")]
        [Display(Name = "ユーザID")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "ユーザ名は必須です。")]
        [Display(Name = "ユーザ名")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "電話番号は必須です。")]
        [Display(Name = "電話番号")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Eメールは必須です。")]
        [EmailAddress]
        [Display(Name = "Eメール")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "パスワードは必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "パスワード")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "変更後Eメールは必須です。")]
        [EmailAddress]
        [Display(Name = "変更後Eメール")]
        public string NewEmail {get; set; } = string.Empty;

        [Required(ErrorMessage = "現在のパスワードは必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "現在のパスワード")]
        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "変更後のパスワードは必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "変更後のパスワード")]
        public string NewPassword1 { get; set; } = string.Empty;

        [Required(ErrorMessage = "変更後のパスワード（再入力）は必須です。")]
        [DataType(DataType.Password)]
        [Display(Name = "変更後のパスワード（再入力）")]
        public string NewPassword2 { get; set; } = string.Empty;

        public int Part { get; set; } = 0;

        public string Notification { get; set; } = string.Empty;

        public bool UpdateResult { get; set; } = true;

    }

    public class UserAdminProfileViewModel : UserAdminViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NewEmail {get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword1 { get; set; } = string.Empty;
        public string NewPassword2 { get; set; } = string.Empty;
    }

    public class UserAdminEmailViewModel : UserAdminViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword1 { get; set; } = string.Empty;
        public string NewPassword2 { get; set; } = string.Empty;
    }

    public class UserAdminPasswordViewModel : UserAdminViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NewEmail {get; set; } = string.Empty;
    }

    public class UserAdminAccountViewModel : UserAdminViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NewEmail {get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword1 { get; set; } = string.Empty;
        public string NewPassword2 { get; set; } = string.Empty;
    }
}
