using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class AddHouseMapImageViewModel
    {
        [HiddenInput]
        public Guid HouseId { get; set; }
        
        [Required]
        public IFormFile Image { get; set; }        
    }
}
