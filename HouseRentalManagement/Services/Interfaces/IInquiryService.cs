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
    }
}
