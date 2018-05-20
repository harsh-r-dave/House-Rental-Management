﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class HouseViewModel
    {
        public Guid HouseId { get; set; }
        public string Address { get; set; }
        public double Rent { get; set; }
    }
}
