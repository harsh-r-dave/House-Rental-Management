using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IFeaturedPhotoRepository
    {
        Task<bool> SaveFeaturedPhotoAsync(FeaturedImage record);
        Task<ICollection<FeaturedImage>> ListAllFeaturedImagesAsync();
        Task<bool> DeleteFeaturedImageAsync(FeaturedImage record);
        Task<FeaturedImage> FetchFeatureImageByIdAsync(int id);
        Task<ICollection<FeaturedImage>> ListToBeDisplayedFeaturedImagesAsync();
    }
}
