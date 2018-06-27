using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IInquiryRepository
    {
        Task<ICollection<Inquiry>> GetReadInquiriesAsync();
        Task<ICollection<Inquiry>> GetUnreadInquiriesAsync();
        Task<Inquiry> GetInquiryByIdAsync(int id);
        Task<bool> SaveInquiryAsync(Inquiry record);
        Task<bool> DeleteAsync(Inquiry inquiry);
        Task<bool> DeleteAllReadInquiriesAsync();
        Task<bool> DeleteAllUnreadInquiriesAsync();
    }
}
