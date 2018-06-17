using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class FeaturedPhotosViewModel
    {
        [Required]
        [Display(Name = "Featured Photo")]
        public IFormFile Image { get; set; }

        [Display(Name = "Display From")]
        public DateTime DisplayFrom { get; set; }

        [Display(Name = "Display Till")]
        public DateTime DisplayTill { get; set; }

        [Display(Name = "Want this image to be displayed?")]
        public bool ToDisplay { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string DisplayFromDateString { get; set; }
        public string DisplayTillDateString { get; set; }

        [HiddenInput]
        public int PhotoId { get; set; }
    }
}
