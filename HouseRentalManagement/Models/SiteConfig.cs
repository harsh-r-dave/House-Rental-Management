using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class SiteConfig
    {
        public int SiteConfigId { get; set; }

        public string PrimaryEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string WhatsappNumber { get; set; }
    }
}
