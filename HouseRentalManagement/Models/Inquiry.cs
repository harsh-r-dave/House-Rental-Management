using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class Inquiry
    {
        public int InquiryId { get; set; }
       
        public string Name { get; set; }
        public string Message { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime CreateDate { get; set; }
        public bool Read { get; set; }
    }
}
