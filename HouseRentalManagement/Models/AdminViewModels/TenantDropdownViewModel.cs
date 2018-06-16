using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class TenantDropdownViewModel
    {
        public Guid TenantId
        {
            get;set;
        }
        public string FullName { get; set; }
    }
}
