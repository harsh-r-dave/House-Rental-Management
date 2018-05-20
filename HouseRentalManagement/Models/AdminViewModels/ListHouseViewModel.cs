﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class ListHouseViewModel
    {
        public ICollection<HouseViewModel> Houses { get; set; } = new HashSet<HouseViewModel>();
    }
}
