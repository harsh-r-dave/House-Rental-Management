using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class AddHouseViewModel
    {
        [HiddenInput]
        public Guid HouseId { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Rent")]
        public double Rent { get; set; }

        [Display(Name = "Date Available")]
        public DateTime DateAvailableFrom { get; set; }
        [Display(Name = "Date Available")]
        public DateTime DateAvailableTo { get; set; }
    }
}
