using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class RestrictionRepository : IRestrictionRepository
    {
        private readonly ApplicationDbContext _context;

        public RestrictionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRestrictionAsync(Restriction restriction)
        {
            if (_context.Entry(restriction).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Add(restriction);
            }
            else
            {
                _context.Update(restriction);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Restriction> FetchByIdAsync(int id)
        {
            return await _context.Restriction.Where(f => f.RestrictionId == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Restriction>> ListRestrictionsAsync()
        {
            return await _context.Restriction.ToListAsync();
        }

        public async Task<bool> DeleteRestrictionAsync(Restriction restriction)
        {
            _context.Restriction.Remove(restriction);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<HouseRestriction>> ListHouseRestrictionsByHouseIdAsync(Guid id)
        {
            return await _context.HouseRestriction.Where(a => a.HouseId == id).ToListAsync();
        }

        public async Task ClearHouseRestrictionsByHouseIdAsync(Guid houseId)
        {
            var facilities = await ListHouseRestrictionsByHouseIdAsync(houseId);
            if (facilities != null)
            {
                foreach (var item in facilities)
                {
                    _context.HouseRestriction.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> SaveHouseRestrictionAsync(HouseRestriction hf)
        {
            _context.HouseRestriction.Add(hf);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
