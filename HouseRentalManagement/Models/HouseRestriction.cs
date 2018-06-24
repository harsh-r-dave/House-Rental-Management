using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class HouseRestriction
    {
        public int HouseRestrictionId { get; set; }

        public int RestrictionId { get; set; }
        public virtual Restriction Restriction { get; set; }

        public Guid HouseId { get; set; }
        public virtual House Houses { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
