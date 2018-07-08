using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.SiteConfigViewModels
{
    public class AddSiteConfigViewModel
    {
        [Required]
        [Display(Name ="Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please provide a valid phone number. eg: (123.123.1234) or (123-123-1234) or (123 123 1234)")]
        public string PhoneNumber { get; set; }

        [Display(Name = "WhatsApp Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please provide a valid phone number. eg: (123.123.1234) or (123-123-1234) or (123 123 1234)")]
        public string WhatsappNumber { get; set; }
        
        public bool IsWhatasappNumberSameAsPhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
