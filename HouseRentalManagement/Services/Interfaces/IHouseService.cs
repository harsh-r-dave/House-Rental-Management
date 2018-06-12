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
        Task<(bool Success, IErrorDictionary Errors, Guid Id)> AddHouseAsync(AddHouseViewModel model);
        Task<(bool Success, IErrorDictionary Errors, EditHouseViewModel Model)> GetEditHouseViewModelAsync(Guid id);
        Task<(bool Success, IErrorDictionary Errors)> DeleteHouseAsync(Guid id);
        Task<(bool Success, IErrorDictionary Errors)> EditHouseAsync(EditHouseViewModel model);
        Task<HouseAmenityViewModel> GetHouseAmenityViewModelAsync(Guid houseId);
        Task<(bool Success, IErrorDictionary Errors)> UpdateHouseAmenitiesAsync(HouseAmenityViewModel model);
        Task<(bool Success, IErrorDictionary Errors)> UploadHouseImageAsync(AddHouseImageViewModel model);
        Task<(bool Success, string Error, bool NoImage, ListHouseImageViewModel Model)> FetchHouseImageListAsync(Guid houseId);
        Task<(bool Success, string Error)> DeleteHouseImageAsync(Guid imageId);
    }
}
