using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class AmenitiesListViewModel
    {
        public int AmenityId { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public string ImageSrc { get; set; }
    }
}
