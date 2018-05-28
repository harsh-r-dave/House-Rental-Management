using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class HouseLeaseLength
    {
        public int HouseLeaseLengthId { get; set; }
        public int LeaseLengthId { get; set; }
        public Guid HouseId { get; set; }

        public ICollection<House> Houses { get; set; }
        public virtual LeaseLength LeaseLength { get; set; }
    }
}
