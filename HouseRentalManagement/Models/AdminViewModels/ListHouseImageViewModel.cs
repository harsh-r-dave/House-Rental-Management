using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class ListHouseImageViewModel
    {
        public ICollection<HouseImageViewModel> HouseImages { get; set; } = new HashSet<HouseImageViewModel>();
    }
}