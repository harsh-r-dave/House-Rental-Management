using HouseRentalManagement.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IRestrictionService
    {
        Task<(bool Success, IErrorDictionary Errors)> AddRestrictionAsync(ManageRestrictionViewModel model);
        Task<(bool Success, IErrorDictionary Errors, ManageRestrictionViewModel Model)> GetManageRestrictionViewModelAsync();
        Task<(bool Success, IErrorDictionary Errors)> DeleteRestrictionAsync(int id);
    }
}
