using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class HouseImageViewModel
    {
        public Guid ImageId { get; set; }
        public Guid HouseId { get; set; }
        public string ImageSrc { get; set; }
        public string fileName { get; set; }
    }
}
