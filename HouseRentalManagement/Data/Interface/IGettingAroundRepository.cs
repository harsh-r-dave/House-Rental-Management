using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IGettingAroundRepository
    {
        Task<bool> SaveGettingAroundAsync(HouseGettingAround record);
        Task<ICollection<HouseGettingAround>> ListGettingAroundByHouseIdAsync(Guid houseId);
        Task<HouseGettingAround> FetchHouseGettingAroundByIdAsync(int id);
        Task<bool> DeleteGettingAroundByIdAsync(HouseGettingAround record);
    }
}
