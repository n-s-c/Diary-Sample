using System.IO;
using System.Text;
using System.Threading.Tasks;
using Diary_Sample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Controllers
{
    [Authorize]
    public class UserAdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserAdminController> _logger;

        private const string EditOkMessage = "更新しました。";
        private const string DeleteOkMessage = "削除しました。";
        private const string EditNgMessage = "エラーが発生して更新できませんでした。";
        private const string DeleteNgMessage = "エラーが発生して削除できませんでした。";

        private const int UserAdminProfilePart = 1;
        private const int UserAdminEmailPart = 2;
        private const int UserAdminPasswordPart = 3;
        private const int UserAdminAccountPart = 4;
        private const int UserAdminAccountDeletePart = 5;

        public UserAdminController(ILogger<UserAdminController> logger,
                                    SignInManager<IdentityUser> signInManager,
                                    UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string emailAddress)
        {
            if (emailAddress == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            var user = await _userManager.FindByEmailAsync(emailAddress).ConfigureAwait(false);
            if (user == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            UserAdminViewModel userAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.PasswordHash,
                Part = UserAdminProfilePart,
            };

            return View("Index", userAdminViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EmailIndex(string userId)
        {
            if (userId == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            UserAdminViewModel userAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.PasswordHash,
                Part = UserAdminEmailPart,
            };

            return View("Index", userAdminViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> PasswordIndex(string userId)
        {
            if (userId == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            UserAdminViewModel userAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Part = UserAdminPasswordPart,
            };

            return View("Index", userAdminViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AccountIndex(string userId)
        {
            if (userId == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            UserAdminViewModel userAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.PasswordHash,
                Part = UserAdminAccountPart,
            };

            return View("Index", userAdminViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteAccountIndex(string userId)
        {
            if (userId == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                // ログイン画面に戻る
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                return RedirectToAction("Index", "Auth");
            }

            UserAdminViewModel userAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.PasswordHash,
                Part = UserAdminAccountDeletePart,
            };

            return View("Index", userAdminViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SaveProfile(UserAdminProfileViewModel userAdminViewModel)
        {
            string userId = userAdminViewModel.UserId;
            string userName = userAdminViewModel.UserName;
            string phoneNumber = userAdminViewModel.PhoneNumber;

            if (!ModelState.IsValid)
            {
                return UpadateError(userAdminViewModel, UserAdminProfilePart, EditNgMessage);
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userAdminViewModel, UserAdminProfilePart, EditNgMessage);
            }

            var identityResult = await _userManager.SetUserNameAsync(user, userName).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminViewModel, UserAdminProfilePart, EditNgMessage);
            }
            identityResult = await _userManager.SetPhoneNumberAsync(user, phoneNumber).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminViewModel, UserAdminProfilePart, EditNgMessage);
            }

            UserAdminViewModel outUserAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.PasswordHash,
                Part = UserAdminProfilePart,
                Notification = EditOkMessage,
                UpdateResult = true,
            };

            return View("Index", outUserAdminViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmail(UserAdminEmailViewModel userAdminViewModel)
        {

            string emailAddress = userAdminViewModel.Email;
            string newEmail = userAdminViewModel.NewEmail;

            if (!ModelState.IsValid)
            {
                return UpadateError(userAdminViewModel, UserAdminEmailPart, EditNgMessage);
            }

            var user = await _userManager.FindByEmailAsync(emailAddress).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userAdminViewModel, UserAdminEmailPart, EditNgMessage);
            }

            string token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail).ConfigureAwait(false);

            var identityResult = await _userManager.ChangeEmailAsync(user, newEmail, token).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminViewModel, UserAdminEmailPart, EditNgMessage);
            }

            UserAdminViewModel outUserAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                Email = newEmail,
                NewEmail = string.Empty,
                Part = UserAdminEmailPart,
                Notification = EditOkMessage,
                UpdateResult = true,
            };

            return View("Index", outUserAdminViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SavePassword(UserAdminPasswordViewModel userAdminViewModel)
        {
            string userId = userAdminViewModel.UserId;
            string oldPassword = userAdminViewModel.OldPassword;
            string newPassword1 = userAdminViewModel.NewPassword1;
            string newPassword2 = userAdminViewModel.NewPassword2;

            if (!ModelState.IsValid)
            {
                return UpadateError(userAdminViewModel, UserAdminPasswordPart, EditNgMessage);
            }

            if (!newPassword1.Equals(newPassword2))
            {
                return UpadateError(userAdminViewModel, UserAdminPasswordPart, EditNgMessage);
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userAdminViewModel, UserAdminPasswordPart, EditNgMessage);
            }

            var identityResult = await _userManager.RemovePasswordAsync(user).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminViewModel, UserAdminPasswordPart, EditNgMessage);
            }
            identityResult = await _userManager.AddPasswordAsync(user, newPassword1).ConfigureAwait(false);
            if (!identityResult.Succeeded)
            {
                return UpadateError(userAdminViewModel, UserAdminPasswordPart, EditNgMessage);
            }

            UserAdminViewModel outUserAdminViewModel = new UserAdminViewModel
            {
                UserId = user.Id,
                Password = newPassword1,
                NewPassword1 = string.Empty,
                NewPassword2 = string.Empty,
                Email = user.Email,
                Part = UserAdminPasswordPart,
                Notification = EditOkMessage,
                UpdateResult = true,
            };

            return View("Index", outUserAdminViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(UserAdminAccountViewModel userAdminViewModel)
        {

            string userId = userAdminViewModel.UserId;
            string password = userAdminViewModel.Password;

            if (!ModelState.IsValid)
            {
                return UpadateError(userAdminViewModel, UserAdminAccountPart, DeleteNgMessage);
            }

            if (userId == null)
            {
                return UpadateError(userAdminViewModel, UserAdminAccountPart, DeleteNgMessage);
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userAdminViewModel, UserAdminAccountPart, DeleteNgMessage);
            }

            bool retPassCheck = await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false);
            if (!retPassCheck)
            {
                return UpadateError(userAdminViewModel, UserAdminAccountPart, DeleteNgMessage);
            }

            var retDelete = await _userManager.DeleteAsync(user).ConfigureAwait(false);
            if (!retDelete.Succeeded)
            {
                return UpadateError(userAdminViewModel, UserAdminAccountPart, DeleteNgMessage);
            }

            await _signInManager.SignOutAsync().ConfigureAwait(false);

            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> AccountDownload(string userId)
        {

            if (userId == null)
            {
                return UpadateError(userId, null, null, null, null, UserAdminAccountPart, DeleteNgMessage);
            }

            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return UpadateError(userId, null, null, null, null, UserAdminAccountPart, DeleteNgMessage);
            }
            var value = "{\"Id\":" + user.Id + 
                        ",\"UserName\":" + user.UserName + 
                        ",\"Email\":" + user.Email + 
                        ",\"EmailConfirmed\":" + user.EmailConfirmed + 
                        ",\"PhoneNumber\":" + user.PhoneNumber + 
                        ",\"PhoneNumberConfirmed\":" + user.PhoneNumberConfirmed + "}";
            var filename = "PersonalData.json";

            return File(new MemoryStream(Encoding.UTF8.GetBytes(value)), "application/json", filename);
        }

        private IActionResult UpadateError(UserAdminViewModel userAdminViewModel,
                                        int part,
                                        string message)
        {
            // インスタンスを生成しないと画面にパラメータが反映されないことがあるので
            UserAdminViewModel outUserAdminViewModel = new UserAdminViewModel();
            outUserAdminViewModel.UserId = userAdminViewModel.UserId;
            outUserAdminViewModel.UserName = userAdminViewModel.UserName;
            outUserAdminViewModel.PhoneNumber = userAdminViewModel.PhoneNumber;
            outUserAdminViewModel.Email = userAdminViewModel.Email;
            outUserAdminViewModel.Password = userAdminViewModel.Password;
            outUserAdminViewModel.NewEmail = userAdminViewModel.NewEmail;
            outUserAdminViewModel.OldPassword = userAdminViewModel.OldPassword;
            outUserAdminViewModel.NewPassword1 = userAdminViewModel.NewPassword1;
            outUserAdminViewModel.NewPassword2 = userAdminViewModel.NewPassword2;
            outUserAdminViewModel.Part = part;
            outUserAdminViewModel.Notification = message;
            outUserAdminViewModel.UpdateResult = false;

            return View("Index", outUserAdminViewModel);
        }

        private IActionResult UpadateError(string userId,
                                        string userName,
                                        string phoneNumber,
                                        string email,
                                        string oldPassword,
                                        int part,
                                        string message)
        {
            UserAdminViewModel userAdminViewModel = new UserAdminViewModel
            {
                UserId = userId,
                UserName = userName,
                PhoneNumber = phoneNumber,
                Email = email,
                Password = oldPassword,
                Part = part,
                Notification = message,
                UpdateResult = false,
            };

            return View("Index", userAdminViewModel);
        }

    }
}