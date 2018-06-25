using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.InquiryViewModels
{
    public class ListInquiryViewModel
    {
        public ICollection<InquiryViewModel> Inquiries { get; set; }
    }
}
