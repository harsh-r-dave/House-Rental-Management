using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.HouseViewModels
{
    public class GettingAroundViewModel
    {
        public string Location { get; set; }
        public decimal? Distance { get; set; }
        public string WalkingTime { get; set; }
        public string BikeTime { get; set; }
        public string CarTime { get; set; }
    }
}
