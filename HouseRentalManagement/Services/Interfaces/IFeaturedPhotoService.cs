using HouseRentalManagement.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IFeaturedPhotoService
    {
        Task<(bool Success, string Error)> UploadFeaturePhotoAsync(FeaturedPhotosViewModel model);
        Task<(bool Success, string Error, ListFeaturedPhotoViewModel Model)> GetListFeaturedPhotosViewModelAsync();
        Task<(bool Success, string Error)> DeleteFeaturedImageAsyncById(int id);
    }
}
