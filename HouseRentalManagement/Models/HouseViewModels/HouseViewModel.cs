using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.HouseViewModels
{
    public class HouseViewModel
    {
        public string FullAddress { get; set; }
        public string Rent { get; set; }
        public string MainImageSrc { get; set; }
        public string Description { get; set; }
        public string DateAvailable { get; set; }
        public string UrlSlug { get; set; }
    }
}
