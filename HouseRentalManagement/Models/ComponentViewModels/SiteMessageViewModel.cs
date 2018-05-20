using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models.ComponentViewModels
{
    public class SiteMessageViewModel
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string WarningMessage { get; set; }
        public string InformationMessage { get; set; }
    }
}
