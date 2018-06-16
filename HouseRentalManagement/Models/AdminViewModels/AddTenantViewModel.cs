using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class AddTenantViewModel
    {
        [HiddenInput]
        public Guid TenantId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Reference Name")]
        public string ReferenceName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Reference Email")]
        public string ReferenceEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Reference Phone Number")]
        public string ReferencePhone { get; set; }

        [Display(Name = "House")]
        public Guid? HouseId { get; set; }

        [Display(Name = "Is on wait list?")]
        public bool IsOnWaitList { get; set; }

        public string FullName { get; set; }
        public string HouseAddress { get; set; }
    }
}
