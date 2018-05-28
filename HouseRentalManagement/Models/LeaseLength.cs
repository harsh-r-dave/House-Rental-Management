using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class LeaseLength
    {
        public int LeaseLengthId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<HouseLeaseLength> HouseLeaseLength { get; set; } = new HashSet<HouseLeaseLength>();
    }
}
