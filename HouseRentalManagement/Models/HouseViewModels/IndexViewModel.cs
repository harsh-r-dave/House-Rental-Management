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
    }
}
