using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class GettingAroundRepository : IGettingAroundRepository
    {
        private readonly ApplicationDbContext _context;
        public GettingAroundRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveGettingAroundAsync(HouseGettingAround record)
        {
            if (_context.Entry(record).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Add(record);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<HouseGettingAround>> ListGettingAroundByHouseIdAsync(Guid houseId)
        {
            return await _context.HouseGettingAround
                .Where(a => a.HouseId == houseId)
                .OrderByDescending(a => a.Create)
                .ToListAsync();
        }

        public async Task<HouseGettingAround> FetchHouseGettingAroundByIdAsync(int id)
        {
            return await _context.HouseGettingAround.Where(a => a.HouseGettingAroundId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteGettingAroundByIdAsync(HouseGettingAround record)
        {
            _context.HouseGettingAround.Remove(record);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
