using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IHouseImageRepository
    {
        Task<bool> SaveHouseImageAsync(HouseImage record);
        Task<ICollection<HouseImage>> FetchHouseImagesAsync(Guid houseId);
    }
}
