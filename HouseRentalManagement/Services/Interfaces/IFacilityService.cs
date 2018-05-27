using HouseRentalManagement.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IFacilityService
    {
        Task<(bool Success, IErrorDictionary Errors)> AddFacilityAsync(ManageFacilityViewModel model);
        Task<(bool Success, IErrorDictionary Errors, ManageFacilityViewModel Model)> GetManageFacilityViewModelAsync();
        Task<(bool Success, IErrorDictionary Errors)> DeleteFacilityAsync(Guid id);
    }
}
