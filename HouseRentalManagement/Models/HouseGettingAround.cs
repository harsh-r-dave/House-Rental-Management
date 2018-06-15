using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class HouseGettingAround
    {
        public int HouseGettingAroundId { get; set; }
        public Guid HouseId { get; set; }
        public string LocationName { get; set; }
        public decimal Distance { get; set; }
        public string WalkingTime { get; set; }
        public string BikeTime { get; set; }
        public string CarTime { get; set; }

        public DateTime Create { get; set; }
        public virtual House House { get; set; }
    }
}
