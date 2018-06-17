using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class FeaturedImage
    {
        public int FeaturedImageId { get; set; }
        public string FileName { get; set; }

        public bool ToDisplay { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}
