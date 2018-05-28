﻿using HouseRentalManagement.Data.Interface;
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
            return await _context.House.Where(h => h.HouseId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteHouseAsync(House house)
        {
            _context.House.Remove(house);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
