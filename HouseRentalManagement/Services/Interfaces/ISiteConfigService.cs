using HouseRentalManagement.Models.SiteConfigViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface ISiteConfigService
    {
        Task<(bool Success, AddSiteConfigViewModel Model)> GetAddSiteConfigViewModelAsync();
        Task<(bool Success, string Error)> SaveSiteConfigAsync(AddSiteConfigViewModel model);
    }
}
