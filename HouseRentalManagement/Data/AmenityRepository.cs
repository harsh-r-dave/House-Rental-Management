using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly ApplicationDbContext _context;

        public AmenityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Amenity>> ListAmenitiesAsync()
        {
            return await _context.Amenity.ToListAsync();
        }

        public async Task<ICollection<HouseAmenity>> ListHouseAmenitiesByHouseIdAsync(Guid id)
        {
            return await _context.HouseAmenity
                .Where(a => a.HouseId == id)
                .Include(a => a.Amenity)
                .ToListAsync();
        }

        public async Task<bool> SaveHouseAmenityAsync(HouseAmenity houseAmenity)
        {
            _context.HouseAmenity.Add(houseAmenity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task ClearHouseAmenitiesByHouseIdAsync(Guid houseId)
        {
            var amenities = await ListHouseAmenitiesByHouseIdAsync(houseId);
            if (amenities != null)
            {
                foreach (var item in amenities)
                {
                    _context.HouseAmenity.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
