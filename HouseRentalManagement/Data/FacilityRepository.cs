using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly ApplicationDbContext _context;

        public FacilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveFacilityAsync(Facility facility)
        {
            if (_context.Entry(facility).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _context.Add(facility);
            }
            else
            {
                _context.Update(facility);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Facility> FetchByIdAsync(Guid id)
        {
            return await _context.Facility.Where(f => f.FacilityId == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Facility>> ListFacilitiesAsync()
        {
            return await _context.Facility.ToListAsync();
        }

        public async Task<bool> DeleteFacilityAsync(Facility facility)
        {
            _context.Facility.Remove(facility);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
