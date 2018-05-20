using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IHouseRepository
    {
        Task<bool> AddHouseAsync(House house);
        Task<ICollection<House>> ListHousesAsync();
    }
}
