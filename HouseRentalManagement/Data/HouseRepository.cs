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

        public async Task<bool> AddHouseAsync(House house)
        {
            await _context.House.AddAsync(house);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<House>> ListHousesAsync()
        {
            return await (from h in _context.House
                          select h)
                          .ToListAsync();
        }
    }
}
