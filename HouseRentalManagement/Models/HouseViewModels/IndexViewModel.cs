using HouseRentalManagement.Models.InquiryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.HouseViewModels
{
    public class IndexViewModel
    {
        public ICollection<FeaturedPhotosViewModel> FeaturedImages { get; set; }
        public ICollection<HouseViewModel> Houses { get; set; }
        public AddInquiryViewModel AddInquiryViewModel { get; set; }

        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactWhatsappNumber { get; set; }
    }
}
