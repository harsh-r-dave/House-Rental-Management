using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.InquiryViewModels
{
    public class AddInquiryViewModel
    {
        [Required(ErrorMessage = "Please enter your Name")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please provide a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please tell us how we can help you")]
        public string Message { get; set; }
    }
}
