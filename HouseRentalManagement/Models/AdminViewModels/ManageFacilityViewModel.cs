using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class ManageFacilityViewModel
    {
        [HiddenInput]
        public Guid? FacilityId { get; set; }

        [Required]
        [Display(Name = "Facility")]
        public string FacilityTitle { get; set; }
    }
}
