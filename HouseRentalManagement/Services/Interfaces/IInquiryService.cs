using HouseRentalManagement.Models.InquiryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IInquiryService
    {
        Task<(bool Success, string Error)> SubmitNewInquiryAsync(AddInquiryViewModel model);
        Task<ListInquiryViewModel> GetUnreadInquiriesAsync();
        Task<ListInquiryViewModel> GetReadInquiriesAsync();
        Task<(bool Success, string Error, string Message, bool IsRead)> GetMessageByIdAsync(int id);
        Task<(bool Success, string Error)> MarkMessageReadByIdAsync(int id);
        Task<(bool Success, string Error)> DeleteInquiryByIdAsync(int id);
        Task<(bool Success, string Error)> DeleteAllUnreadInquiryAsync();
        Task<(bool Success, string Error)> DeleteAllReadInquiryAsync();
    }
}
