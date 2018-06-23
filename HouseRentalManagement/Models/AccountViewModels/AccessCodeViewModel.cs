using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AccountViewModels
{
    public class AccessCodeViewModel
    {
        [Required]
        [Display(Name = "Access Code")]
        public string AccessCode { get; set; }
    }
}
