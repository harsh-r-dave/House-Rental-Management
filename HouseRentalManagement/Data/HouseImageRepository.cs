﻿using HouseRentalManagement.Data.Interface;
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

        public async Task<ICollection<HouseImage>> ListHouseImagesAsync(Guid houseId)
        {
            return await _context.HouseImage.Where(a=>a.HouseId == houseId).OrderByDescending(a=>a.IsHomePageImage).ThenByDescending(a=>a.CreateUtc).ToListAsync();
        }

        public async Task<HouseImage> FetchHouseImageByHouseImageId(Guid imageId)
        {
            return await _context.HouseImage.Where(a => a.HouseImageId == imageId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteHouseImageAsync(HouseImage houseImage)
        {
            _context.HouseImage.Remove(houseImage);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<HouseImage> GetMainImageByHouseIdAsync(Guid houseId)
        {
            return await _context.HouseImage
                .Where(a => a.HouseId == houseId && (a.IsHomePageImage.HasValue && a.IsHomePageImage.Value))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveHouseMapImageAsync(HouseMapImage record)
        {
            if (_context.Entry(record).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Add(record);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<HouseMapImage> FetchMapImageByHouseIdAsync(Guid houseId)
        {
            return await  _context.HouseMapImage.Where(a => a.HouseId == houseId).FirstOrDefaultAsync();
        }

        public async Task<HouseMapImage> FetchMapImageByImageIdAsync(Guid imageId)
        {
            return await _context.HouseMapImage.Where(a => a.HouseMapImageId == imageId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteHouseMapImageAsync(HouseMapImage image)
        {
            _context.HouseMapImage.Remove(image);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
