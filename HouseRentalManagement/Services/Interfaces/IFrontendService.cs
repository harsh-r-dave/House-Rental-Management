using HouseRentalManagement.Models.HouseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IFrontendService
    {
        Task<IndexViewModel> GetIndexViewModelAsync();
    }
}
