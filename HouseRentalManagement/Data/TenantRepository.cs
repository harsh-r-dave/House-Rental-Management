using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
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
                          select t)
                          .Include(a=>a.House)
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
    }
}
