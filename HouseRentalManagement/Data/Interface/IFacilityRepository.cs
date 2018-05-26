using HouseRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Data.Interface
{
    public interface IFacilityRepository
    {
        Task<bool> SaveFacilityAsync(Facility facility);
        Task<Facility> FetchByIdAsync(Guid id);
    }
}
