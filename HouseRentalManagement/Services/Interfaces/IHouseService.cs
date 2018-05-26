using HouseRentalManagement.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IHouseService
    {
        Task<(bool Success, ListHouseViewModel Model)> ListHousesAsync();
        Task<(bool Success, IErrorDictionary Errors)> AddHouseAsync(AddHouseViewModel model);
        Task<(bool Success, IErrorDictionary Errors)> AddFacilityAsync(ManageFacilityViewModel model);
    }
}
