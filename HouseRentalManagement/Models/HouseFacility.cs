using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class HouseFacility
    {
        public Guid HouseFacilityId { get; set; }

        public Guid HouseId { get; set; }
        public virtual House House { get; set; }

        public Guid FacilityId { get; set; }
        public virtual Facility Facility { get; set; }

        public DateTime CreateUtc { get; set; }
    }
}
