using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class AddHouseGettingAroundViewModel
    {
        [HiddenInput]
        public int GetingAroundId { get; set; }

        [HiddenInput]
        public Guid HouseId { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Distance (in KMs)")]
        public decimal? Distance { get; set; }

        [Display(Name = "Walk Time")]
        public string WalkingTime { get; set; }

        [Display(Name = "Bike Time")]
        public string BikeTime { get; set; }

        [Display(Name = "Car Time")]
        public string CarTime { get; set; }
    }
}
