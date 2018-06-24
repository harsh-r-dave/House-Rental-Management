using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IRestrictionRepository
    {
        Task<bool> SaveRestrictionAsync(Restriction restriction);
        Task<Restriction> FetchByIdAsync(int id);
        Task<ICollection<Restriction>> ListRestrictionsAsync();
        Task<bool> DeleteRestrictionAsync(Restriction restriction);
        Task<ICollection<HouseRestriction>> ListHouseRestrictionsByHouseIdAsync(Guid id);
        Task ClearHouseRestrictionsByHouseIdAsync(Guid houseId);
        Task<bool> SaveHouseRestrictionAsync(HouseRestriction hr);
    }
}
