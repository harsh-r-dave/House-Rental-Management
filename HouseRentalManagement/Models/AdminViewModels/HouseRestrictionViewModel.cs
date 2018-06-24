using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class HouseRestrictionViewModel
    {
        public HouseRestrictionViewModel()
        {
            Restrictions = new HashSet<RestrictionsListViewModel>();
        }

        [HiddenInput]
        public Guid HouseId { get; set; }
        public ICollection<RestrictionsListViewModel> Restrictions { get; set; }
    }
}
