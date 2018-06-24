using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class Restriction
    {
        public int RestrictionId { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
