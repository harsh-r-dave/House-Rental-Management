using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AdminViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ApplicationDbContext _context;

        public TenantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, Guid id)> AddTenantAsync(Tenant tenant)
        {
            if (_context.Entry(tenant).State == EntityState.Detached)
            {
                _context.Add(tenant);
            }
            //await _context.Tenant.AddAsync(tenant);
            var success = await _context.SaveChangesAsync() > 0;
            var id = tenant.TenantId;

            // consider it as successful transaction if entity is not changed
            if (_context.Entry(tenant).State == EntityState.Unchanged)
            {
                success = true;
            }

            return (success, id);
        }

        public async Task<bool> UpdateTenantAsync(Tenant Tenant)
        {
            _context.Tenant.Update(Tenant);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Tenant>> ListTenantsAsync()
        {
            return await (from t in _context.Tenant
                          where !t.IsOnWaitList
                          orderby t.LastName
                          select t)
                          .Include(a=>a.House)
                          .ToListAsync();
        }

        public async Task<ICollection<Tenant>> ListTenantWaitListAsync() {
            return await (from t in _context.Tenant
                          orderby t.LastName
                          where t.IsOnWaitList
                          select t)
                          .Include(a => a.House)
                          .ToListAsync();
        }

        public async Task<Tenant> FetchTenantByIdAsync(Guid id)
        {
            return await _context.Tenant.Where(h => h.TenantId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTenantAsync(Tenant Tenant)
        {
            _context.Tenant.Remove(Tenant);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Tenant>> FetchTenantsListByHouseIdAsync(Guid houseId)
        {
            return await _context.Tenant.OrderBy(t=>t.LastName).Where(a => a.HouseId == houseId).ToListAsync();
        }

        public async Task<ICollection<TenantDropdownViewModel>> FetchTenantListForHouseEditPageAsync(Guid houseId)
        {
            return await (from t in _context.Tenant
                          orderby t.LastName
                          where t.HouseId != houseId && !t.IsOnWaitList
                          select new TenantDropdownViewModel()
                          {
                              FullName = $"{t.LastName}, {t.FirstName}",
                              TenantId = t.TenantId
                          })
                          .ToListAsync();
                          
        }

        public async Task<ICollection<TenantDropdownViewModel>> GetTenantWaitListDropdownAsync()
        {
            return await (from t in _context.Tenant
                          orderby t.LastName
                          where t.IsOnWaitList
                          select new TenantDropdownViewModel()
                          {
                              FullName = $"{t.LastName}, {t.FirstName}",
                              TenantId = t.TenantId
                          })
                          .ToListAsync();

        }
    }
}
