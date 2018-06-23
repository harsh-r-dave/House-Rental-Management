using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class HouseFacilityViewModel
    {
        public HouseFacilityViewModel()
        {
            Facilities = new HashSet<FacilitiesListViewModel>();
        }

        [HiddenInput]
        public Guid HouseId { get; set; }
        public ICollection<FacilitiesListViewModel> Facilities { get; set; }
    }
}
