using System.Threading.Tasks;
using Diary_Sample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Controllers
{
    [Authorize]
    public class UserAdminPasswordController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserAdminPasswordController> _logger;

        private const string EditOkMessage = "更新しました。";
        private const string EditNgMessage = "エラーが発生して更新できませんでした。";
        private const string PasswordMismatch = "変更後のパスワードと変更後のパスワード（再入力）が不一致です。";
        private const string PasswordMisstake = "現在のパスワードが間違っています。";

        public UserAdminPasswordController(ILogger<UserAdminPasswordController> logger,
                                    SignInManager<IdentityUser> signInManager,
                                    UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            UserAdminPasswordViewModel userAdminPasswordViewModel = new UserAdminPasswordViewModel
            {
                UserId = user.Id,
                OldPassword = string.Empty,
                NewPassword1 = string.Empty,
                NewPassword2 = string.Empty,
            };

            return View("Index", userAdminPasswordViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserAdminPasswordViewModel userAdminPasswordViewModel)
        {
            string oldPassword = userAdminPasswordViewModel.OldPassword;
            string newPassword1 = userAdminPasswordViewModel.NewPassword1;
            string newPassword2 = userAdminPasswordViewModel.NewPassword2;

            if (!ModelState.IsValid)
            {
                return UpadateError(userAdminPasswordViewModel, EditNgMessage);
            }

            if (!newPassword1.Equals(newPassword2))
            {
                return UpadateError(userAdminPasswordViewModel, PasswordMismatch);
            }

            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userAdminPasswordViewModel, EditNgMessage);
            }

            // 念のため変更前のパスワードをチェックする
            bool ret = await _userManager.CheckPasswordAsync(user, oldPassword).ConfigureAwait(false);
            if (!ret)
            {
                return UpadateError(userAdminPasswordViewModel, PasswordMisstake);
            }

            var identityResult = await _userManager.RemovePasswordAsync(user).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminPasswordViewModel, EditNgMessage);
            }
            identityResult = await _userManager.AddPasswordAsync(user, newPassword1).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminPasswordViewModel, EditNgMessage);
            }

            UserAdminPasswordViewModel outUserAdminPasswordViewModel = new UserAdminPasswordViewModel
            {
                UserId = user.Id,
                NewPassword1 = string.Empty,
                NewPassword2 = string.Empty,
                Notification = EditOkMessage,
                UpdateResult = true,
            };
            
            return View("Index", outUserAdminPasswordViewModel);
        }

        private IActionResult UpadateError(UserAdminPasswordViewModel userAdminPasswordViewModel, string message)
        {
            UserAdminPasswordViewModel outUserAdminProfileViewModel = new UserAdminPasswordViewModel
            {
                UserId = userAdminPasswordViewModel.UserId,
                OldPassword = userAdminPasswordViewModel.OldPassword,
                NewPassword1 = userAdminPasswordViewModel.NewPassword1,
                NewPassword2 = userAdminPasswordViewModel.NewPassword2,
                Notification = message,
                UpdateResult = false,
            };

            return View("Index", outUserAdminProfileViewModel);
        }

    }
}