using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IHouseRepository
    {
        Task<(bool Success, Guid id)> AddHouseAsync(House house);
        Task<bool> UpdateHouseAsync(House house);
        Task<ICollection<House>> ListHousesAsync();
        Task<House> FetchHouseByIdAsync(Guid id);
        Task<bool> DeleteHouseAsync(House house);
        Task<ICollection<House>> GetHouseListForIndexPageAsync();
    }
}
