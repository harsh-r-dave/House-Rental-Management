using HouseRentalManagement.Models.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IHouseService
    {
        Task<(bool Success, ListHouseViewModel Model)> ListHousesAsync();
    }
}
