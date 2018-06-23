using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class HouseViewModel
    {
        public Guid HouseId { get; set; }
        public string Address { get; set; }
        public decimal Rent { get; set; }
        public bool IsDisplaying { get; set; }
    }
}
