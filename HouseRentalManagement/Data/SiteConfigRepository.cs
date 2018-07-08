using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class SiteConfigRepository : ISiteConfigRepository
    {
        private readonly ApplicationDbContext _context;

        public SiteConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SiteConfig> GetSiteConfigAsync()
        {
            return await _context.SiteConfig.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveSiteConfigAsync(SiteConfig record)
        {
            if (_context.Entry(record).State == EntityState.Unchanged)
            {
                return true;
            }

            if (_context.Entry(record).State == EntityState.Detached)
            {
                _context.Add(record);
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
