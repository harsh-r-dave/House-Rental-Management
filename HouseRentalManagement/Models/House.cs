using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class House
    {
        public Guid HouseId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string Description { get; set; }
        public decimal Rent { get; set; }

        public DateTime CreateUtc { get; set; }
        public DateTime AuditUtc { get; set; }

        public virtual ICollection<HouseFacility> Facilities { get; set; } = new HashSet<HouseFacility>();
        public virtual ICollection<Tenant> Tenants { get; set; } = new HashSet<Tenant>();
        public virtual ICollection<HouseImage> HouseImages { get; set; } = new HashSet<HouseImage>();
    }
}
