using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.InquiryViewModels
{
    public class InquiryViewModel
    {
        public int InquiryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime InquiryDate { get; set; }
        public bool IsRead { get; set; }
    }
}
