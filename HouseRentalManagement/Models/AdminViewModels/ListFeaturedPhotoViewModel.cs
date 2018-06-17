using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class ListFeaturedPhotoViewModel
    {
        public ICollection<FeaturedPhotosViewModel> featuredPhotosCollection { get; set; }
    }
}
