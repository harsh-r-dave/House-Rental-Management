using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class HouseRepository : IHouseRepository
    {
        private readonly ApplicationDbContext _context;

        public HouseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, Guid id)> AddHouseAsync(House house)
        {
            await _context.House.AddAsync(house);
            var success = await _context.SaveChangesAsync() > 0;
            var id = house.HouseId;

            return (success, id);
        }

        public async Task<bool> UpdateHouseAsync(House house)
        {
            _context.House.Update(house);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<House>> ListHousesAsync()
        {
            return await (from h in _context.House
                          select h)
                          .ToListAsync();
        }

        public async Task<House> FetchHouseByIdAsync(Guid id)
        {
            return await _context.House
                .Where(h => h.HouseId == id)
                .Include(a => a.HouseImages)
                .Include(a => a.MapImages)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteHouseAsync(House house)
        {
            _context.House.Remove(house);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<House>> GetHouseListForIndexPageAsync()
        {
            return await _context.House
                .Where(a => a.IsDisplaying)
                .Include(a => a.HouseImages)
                .ToListAsync();
        }

        public async Task<House> GetHouseByIdOrSlugAsync(string slug = "", Guid? id = null)
        {
            return await (from h in _context.House
                          where h.HouseId == id || string.Equals(h.UrlSlug, slug, StringComparison.InvariantCultureIgnoreCase)
                          && h.IsDisplaying
                          select h)
                          .Include(h => h.HouseImages)
                          .Include(h => h.HouseAmenities)
                            .ThenInclude(a => a.Amenity)
                          .Include(h => h.HouseRestrictions)
                            .ThenInclude(r => r.Restriction)
                          .Include(h => h.HouseGettingArounds)
                          .Include(h => h.Facilities)
                            .ThenInclude(f => f.Facility)
                          .FirstOrDefaultAsync();
        }

        public async Task<House> GetHouseByIdForPreviewAsync(Guid id)
        {
            return await (from h in _context.House
                          where h.HouseId == id
                          select h)
                          .Include(h => h.HouseImages)
                          .Include(h => h.HouseAmenities)
                            .ThenInclude(a => a.Amenity)
                          .Include(h => h.HouseRestrictions)
                            .ThenInclude(r => r.Restriction)
                          .Include(h => h.HouseGettingArounds)
                          .Include(h => h.Facilities)
                            .ThenInclude(f => f.Facility)
                          .FirstOrDefaultAsync();
        }
    }
}
