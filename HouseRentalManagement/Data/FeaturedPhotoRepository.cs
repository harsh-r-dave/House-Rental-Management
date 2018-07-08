using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class FeaturedPhotoRepository : IFeaturedPhotoRepository
    {
        private readonly ApplicationDbContext _context;

        public FeaturedPhotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveFeaturedPhotoAsync(FeaturedImage record)
        {
            if (_context.Entry(record).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Add(record);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<FeaturedImage>> ListAllFeaturedImagesAsync()
        {
            return await _context.FeaturedImage.OrderByDescending(a => a.ToDisplay).ThenByDescending(a => a.CreatedUtc).ToListAsync();
        }

        public async Task<FeaturedImage> FetchFeatureImageByIdAsync(int id)
        {
            return await _context.FeaturedImage.Where(a => a.FeaturedImageId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteFeaturedImageAsync(FeaturedImage record)
        {
            _context.FeaturedImage.Remove(record);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<FeaturedImage>> ListToBeDisplayedFeaturedImagesAsync()
        {
            return await _context.FeaturedImage.OrderByDescending(a=>a.CreatedUtc).Where(a => a.ToDisplay).ToListAsync();
        }
    }
}
