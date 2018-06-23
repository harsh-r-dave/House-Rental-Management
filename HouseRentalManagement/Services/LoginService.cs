using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AccountViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IAccessCodeRepository _accessCodeRepository;

        public LoginService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginService> logger,
            IAccessCodeRepository accessCodeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _accessCodeRepository = accessCodeRepository;
        }

        public async Task<(bool Success, string Error)> LogUserIn(LoginViewModel model)
        {
            bool success = false;
            string error = string.Empty;

            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                success = true;
            }
            else if (result.IsLockedOut)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var lockoutEndTime = user.LockoutEnd;
                var remainingTime = (lockoutEndTime.Value - DateTime.UtcNow).TotalMinutes;
                error = $"Your account has been locked for {Math.Ceiling(remainingTime)} minutes due to invalid login attempts.";
            }
            else
            {
                error = "Invalid login attempt.";
            }

            return (success, error);
        }

        public async Task<bool> VerifyAccessCodeAsync(AccessCodeViewModel model)
        {
            bool success = false;
            try
            {
                var accessCode = await _accessCodeRepository.GetAccessCodeAsync();
                if (accessCode != null)
                {
                    var resultManager = new Helper.EncryptionHelper.EncryptionManager();
                    var hash = resultManager.GeneratePasswordHash(model.AccessCode, out string salt);
                    success = resultManager.IsStringMatch(model.AccessCode, accessCode.Salt, accessCode.Hash);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("LoginService/VerifyAccessCodeAsync - exception:{@Ex}", new object[]{
                    ex
                });
            }


            return success;
        }

        public async Task<bool> SetAccessCodeAsync(AccessCodeViewModel model)
        {
            bool success = false;

            try
            {
                // make sure there is only one access code
                var accessCode = await _accessCodeRepository.GetAccessCodeAsync();
                if (accessCode == null)
                {
                    accessCode = new AccessCode()
                    {
                        CreateDate = DateTime.UtcNow
                    };
                }
                // encrypt code
                var encryptionManager = new Helper.EncryptionHelper.EncryptionManager();
                var hash = encryptionManager.GeneratePasswordHash(model.AccessCode, out string salt);
                // save to db                
                accessCode.Salt = salt;
                accessCode.Hash = hash;

                success = await _accessCodeRepository.SaveAccessCodeAsync(accessCode);
            }
            catch (Exception ex)
            {
                _logger.LogError("LoginService/SetAccessCodeAsync - exception:{@Ex}", new object[]{
                    ex
                });
            }
            return success;
        }
    }
}
