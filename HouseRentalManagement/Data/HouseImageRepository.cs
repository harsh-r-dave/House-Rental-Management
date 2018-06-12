using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class HouseImageRepository : IHouseImageRepository
    {
        private readonly ApplicationDbContext _context;

        public HouseImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveHouseImageAsync(HouseImage record)
        {
            if (_context.Entry(record).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Add(record);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<HouseImage>> FetchHouseImagesAsync(Guid houseId)
        {
            return await _context.HouseImage.Where(a=>a.HouseId == houseId).OrderByDescending(a=>a.CreateUtc).ToListAsync();
        }
    }
}
