using HouseRentalManagement.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface ITenantService
    {
        Task<(bool Success, ListTenantViewModel Model)> ListTenantsAsync();
        Task<(bool Success, IErrorDictionary Errors, Guid Id)> AddTenantAsync(AddTenantViewModel model);
        Task<(bool Success, IErrorDictionary Errors, AddTenantViewModel Model)> GetEditTenantViewModelAsync(Guid id);
        Task<(bool Success, IErrorDictionary Errors)> DeleteTenantAsync(Guid id);
        Task<(bool Success, String Error, bool NoTenants, ListTenantViewModel Model)> FetchHouseTenantListAsync(Guid houseId);
        Task<(bool Success, string Error)> AddTenantToHouseAsync(Guid houseId, Guid tenantId);
        Task<ICollection<TenantDropdownViewModel>> GetTenantListForHouseEditPageAsync(Guid houseId);
        Task<(bool Success, string Error)> RemoveTenantFromHouseAsync(Guid tenantId);
    }
}
