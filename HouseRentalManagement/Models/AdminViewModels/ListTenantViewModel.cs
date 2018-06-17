using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class ListTenantViewModel
    {
        public ICollection<AddTenantViewModel> TenantCollection = new HashSet<AddTenantViewModel>();
        public ICollection<AddTenantViewModel> TenantWaitListCollection = new HashSet<AddTenantViewModel>();
    }
}
