using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class EditHouseViewModel
    {
        [HiddenInput]
        public Guid HouseId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [RegularExpression("[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy][0-9][ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxyz] ?[0-9][ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxyz][0-9]", ErrorMessage = "Postal code format is invalid")]
        public string PostalCode { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Rent")]
        public decimal? Rent { get; set; }

        [Display(Name = "Date Available")]
        public DateTime? DateAvailableFrom { get; set; }
        [Display(Name = "Date Available")]
        public DateTime? DateAvailableTo { get; set; }

        [Display(Name ="Parking Space")]
        public int ParkingSpace { get; set; }

        public ICollection<EditFacilityViewModel> Facilities { get; set; }
        public ICollection<AmenitiesListViewModel> Amenities { get; set; }
        public AddHouseImageViewModel AddHouseImageViewModel { get; set; }
        public AddHouseGettingAroundViewModel AddHouseGettingAroundViewModel { get; set; }

    }
}
