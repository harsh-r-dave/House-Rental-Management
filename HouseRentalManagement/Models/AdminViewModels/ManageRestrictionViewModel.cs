using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class ManageRestrictionViewModel
    {
        [HiddenInput]
        public int? RestrictionId { get; set; }

        [Required]
        [Display(Name = "Restriction")]
        public string RestrictionTitle { get; set; }

        public ICollection<ListRestrictionViewModel> ListRestrictionViewModel { get; set; } = new List<ListRestrictionViewModel>();
    }
}
