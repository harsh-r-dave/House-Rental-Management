using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface ITenantRepository
    {
        Task<(bool Success, Guid id)> AddTenantAsync(Tenant Tenant);
        Task<bool> UpdateTenantAsync(Tenant Tenant);
        Task<ICollection<Tenant>> ListTenantsAsync();
        Task<Tenant> FetchTenantByIdAsync(Guid id);
        Task<bool> DeleteTenantAsync(Tenant Tenant);
        Task<ICollection<Tenant>> FetchTenantsListByHouseIdAsync(Guid houseId);
        Task<ICollection<TenantDropdownViewModel>> FetchTenantListForHouseEditPageAsync(Guid houseId);
        Task<ICollection<TenantDropdownViewModel>> GetTenantWaitListDropdownAsync();
    }
}
