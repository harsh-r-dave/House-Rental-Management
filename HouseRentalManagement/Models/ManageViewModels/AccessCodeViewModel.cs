using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.ManageViewModels
{
    public class AccessCodeViewModel
    {
        [Required]
        [Display(Name = "Current Access Code")]
        public string CurrentAccessCode { get; set; }

        [Required]
        [Display(Name = "New Access Code")]
        public string NewAccessCode { get; set; }

        public string StatusMessage { get; set; }
    }
}
