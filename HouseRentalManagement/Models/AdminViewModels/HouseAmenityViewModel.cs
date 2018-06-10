using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class HouseAmenityViewModel
    {
        public HouseAmenityViewModel()
        {
            Amenities = new HashSet<AmenitiesListViewModel>();
        }

        [HiddenInput]
        public Guid HouseId { get; set; }
        public ICollection<AmenitiesListViewModel> Amenities { get; set; }
    }
}
