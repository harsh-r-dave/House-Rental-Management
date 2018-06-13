using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class AddHouseImageViewModel
    {
        [HiddenInput]
        public Guid HouseId { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public bool IsHomePageImage { get; set; }
    }
}
