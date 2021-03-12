using System.Threading.Tasks;
using Diary_Sample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Controllers
{
    [Authorize]
    public class UserAdminEmailController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserAdminEmailController> _logger;

        private const string EditOkMessage = "更新しました。";
        private const string EditNgMessage = "エラーが発生して更新できませんでした。";

        public UserAdminEmailController(ILogger<UserAdminEmailController> logger,
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

            UserAdminEmailViewModel userAdminEmailViewModel = new UserAdminEmailViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                NewEmail = string.Empty,
            };

            return View("Index", userAdminEmailViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(UserAdminEmailViewModel userAdminEmailViewModel)
        {
            string newEmail = userAdminEmailViewModel.NewEmail;

            if (!ModelState.IsValid)
            {
                return UpadateError(userAdminEmailViewModel, EditNgMessage);
            }

            var user = await _userManager.GetUserAsync(User).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userAdminEmailViewModel, EditNgMessage);
            }

            string token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail).ConfigureAwait(false);

            var identityResult = await _userManager.ChangeEmailAsync(user, newEmail, token).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminEmailViewModel, EditNgMessage);
            }

            UserAdminEmailViewModel outUserAdminEmailViewModel = new UserAdminEmailViewModel
            {
                UserId = user.Id,
                Email = newEmail,
                NewEmail = string.Empty,
                Notification = EditOkMessage,
                UpdateResult = true,
            };
            
            return View("Index", outUserAdminEmailViewModel);
        }

        private IActionResult UpadateError(UserAdminEmailViewModel userAdminEmailViewModel, string message)
        {
            UserAdminEmailViewModel outUserAdminEmailViewModel = new UserAdminEmailViewModel
            {
                UserId = userAdminEmailViewModel.UserId,
                Email = userAdminEmailViewModel.Email,
                NewEmail = userAdminEmailViewModel.NewEmail,
                Notification = message,
                UpdateResult = false,
            };

            return View("Index", outUserAdminEmailViewModel);
        }
    }
}