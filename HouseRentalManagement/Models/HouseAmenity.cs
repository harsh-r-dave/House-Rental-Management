using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class HouseAmenity
    {
        public int HouseAmenityId { get; set; }
        public Guid HouseId { get; set; }
        public int AmenityId { get; set; }

        public ICollection<House> Houses { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
    }
}
