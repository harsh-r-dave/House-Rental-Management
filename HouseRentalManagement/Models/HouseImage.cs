using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class HouseImage
    {
        public Guid HouseImageId { get; set; }
        public string FileName { get; set; }
        public bool? InUse { get; set; }
        public DateTime CreateUtc { get; set; }

        public Guid HouseId { get; set; }
        public virtual House House { get; set; }
    }
}
