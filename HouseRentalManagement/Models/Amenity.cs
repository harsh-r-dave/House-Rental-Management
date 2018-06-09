using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }

        public ICollection<HouseAmenity> HouseAmenities { get; set; } = new HashSet<HouseAmenity>();
        public ICollection<House> Houses { get; set; } = new HashSet<House>();
    }
}
