using System.Threading.Tasks;
using Diary_Sample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Controllers
{
    [Authorize]
    public class UserAdminProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserAdminProfileController> _logger;

        private const string EditOkMessage = "更新しました。";
        private const string EditNgMessage = "エラーが発生して更新できませんでした。";

        public UserAdminProfileController(ILogger<UserAdminProfileController> logger,
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

            UserAdminProfileViewModel userAdminProfileViewModel = new UserAdminProfileViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
            };

            return View("Index", userAdminProfileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserAdminProfileViewModel userAdminProfileViewModel)
        {
            string userName = userAdminProfileViewModel.UserName;
            string phoneNumber = userAdminProfileViewModel.PhoneNumber;

            if (!ModelState.IsValid)
            {
                // IsValidだとRequiredを付けていない電話番号もチェックがかかる。
                // バリデーションのエラー文言は出したいので、
                // ユーザ名がnullの時だけエラーにする。
                if (userName == null)
                {
                    return UpadateError(userAdminProfileViewModel, EditNgMessage);
                }
            }

            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userAdminProfileViewModel, EditNgMessage);
            }

            var identityResult = await _userManager.SetUserNameAsync(user, userName).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminProfileViewModel, EditNgMessage);
            }
            identityResult = await _userManager.SetPhoneNumberAsync(user, phoneNumber).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminProfileViewModel, EditNgMessage);
            }

            UserAdminProfileViewModel outUserAdminProfileViewModel = new UserAdminProfileViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Notification = EditOkMessage,
                UpdateResult = true,
            };

            return View("Index", outUserAdminProfileViewModel);
        }

        private IActionResult UpadateError(UserAdminProfileViewModel userAdminProfileViewModel, string message)
        {
            UserAdminProfileViewModel outUserAdminProfileViewModel = new UserAdminProfileViewModel
            {
                UserId = userAdminProfileViewModel.UserId,
                UserName = userAdminProfileViewModel.UserName,
                PhoneNumber = userAdminProfileViewModel.PhoneNumber,
                Notification = message,
                UpdateResult = false,
            };

            return View("Index", outUserAdminProfileViewModel);
        }
    }

}