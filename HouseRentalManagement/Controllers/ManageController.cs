using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.ManageViewModels;
using HouseRentalManagement.Services;
using HouseRentalManagement.Services.Interfaces;

namespace HouseRentalManagement.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : HrmController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ILoginService _loginService;

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ILogger<ManageController> logger,
          ILoginService loginService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _loginService = loginService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult AccessCode()
        {
            var model = new AccessCodeViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccessCode(AccessCodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _loginService.ResetAccessCodeAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Access code has been changed successfully");
                    model.StatusMessage = "Access code has been changed.";
                    return RedirectToAction(nameof(AccessCode));
                }
                else
                {
                    SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, result.Error);
                    model.StatusMessage = result.Error;
                }
            }

            return View(model);
        }

       

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Your password has been changed.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }
        

        #endregion
    }
}
