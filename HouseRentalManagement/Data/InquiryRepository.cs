using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data
{
    public class InquiryRepository : IInquiryRepository
    {
        private readonly ApplicationDbContext _context;

        public InquiryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Inquiry>> GetUnreadInquiriesAsync()
        {
            return await _context.Inquiry.Where(a => !a.Read).OrderByDescending(a=>a.InquiryId).ToListAsync();
        }

        public async Task<ICollection<Inquiry>> GetReadInquiriesAsync()
        {
            return await _context.Inquiry.Where(a => a.Read).OrderByDescending(a=>a.InquiryId).ToListAsync();
        }

        public async Task<Inquiry> GetInquiryByIdAsync(int id)
        {
            return await _context.Inquiry.Where(a => a.InquiryId == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveInquiryAsync(Inquiry record)
        {
            if (_context.Entry(record).State == EntityState.Detached)
            {
                _context.Add(record);
            }
            if (_context.Entry(record).State == EntityState.Unchanged)
            {
                return true;
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Inquiry inquiry)
        {
            _context.Inquiry.Remove(inquiry);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
