using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.AdminViewModels
{
    public class EditFacilityViewModel
    {
        public Guid FacilityId { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }
}
