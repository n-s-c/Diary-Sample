// -----------------------------------------------------------------------
// <copyright file="AuthController.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Diary_Sample.Models;
using Diary_Sample.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diary_Sample.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AuthController> _logger;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public AuthController(SignInManager<IdentityUser> signInManager,
            ILogger<AuthController> logger,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AuthViewModel model = new AuthViewModel();

            // Clear the existing external cookie to ensure a clean Auth process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count Auth failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Menu");
                }
                // if (result.RequiresTwoFactor)
                // {
                //     return RedirectToPage("./AuthWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                // }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    model.Notification = "ログインに失敗しました";
                    return View("Index", model);
                }
            }

            // ここまで来たら何か失敗した、ログインへ戻る
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> Unauthorized()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return View("Unauthorized");
        }
    }
}