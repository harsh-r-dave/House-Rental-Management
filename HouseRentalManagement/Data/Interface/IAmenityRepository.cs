using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IAmenityRepository
    {
        Task<ICollection<Amenity>> ListAmenitiesAsync();
        Task<ICollection<HouseAmenity>> ListHouseAmenitiesByHouseIdAsync(Guid id);
        Task<bool> SaveHouseAmenityAsync(HouseAmenity houseAmenity);
        Task ClearHouseAmenitiesByHouseIdAsync(Guid houseId);
    }
}
