using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class Facility
    {
        public Guid FacilityId { get; set; }
        public string Name { get; set; }
        public DateTime CreateUtc { get; set; }
    }
}
