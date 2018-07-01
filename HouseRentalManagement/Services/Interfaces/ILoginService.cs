using HouseRentalManagement.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface ILoginService
    {
        Task<(bool Success, string Error)> LogUserIn(LoginViewModel model);
        Task<bool> VerifyAccessCodeAsync(AccessCodeViewModel model);
        Task<bool> SetAccessCodeAsync(AccessCodeViewModel model);
        Task<(bool Success, string Error)> ResetAccessCodeAsync(Models.ManageViewModels.AccessCodeViewModel model);
    }
}
