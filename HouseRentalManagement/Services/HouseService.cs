using HouseRentalManagement.Models.AdminViewModels;
using HouseRentalManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services
{
    public class HouseService : IHouseService
    {
        public HouseService()
        {

        }

        public async Task<(bool Success, ListHouseViewModel Model)> ListHousesAsync()
        {
            bool success = false;
            var model = new ListHouseViewModel();

            try
            {
                model.Houses.Add(new HouseViewModel()
                {
                    HouseId = new Guid(),
                    Address = "123 Main Street",
                    Rent = 350.00
                });
                model.Houses.Add(new HouseViewModel()
                {
                    HouseId = new Guid(),
                    Address = "123 Main Street",
                    Rent = 300.00
                });
                model.Houses.Add(new HouseViewModel()
                {
                    HouseId = new Guid(),
                    Address = "456 Toronto Street",
                    Rent = 350.00
                });
                model.Houses.Add(new HouseViewModel()
                {
                    HouseId = new Guid(),
                    Address = "11 College Cr",
                    Rent = 450.00
                });
                model.Houses.Add(new HouseViewModel()
                {
                    HouseId = new Guid(),
                    Address = "Some big house",
                    Rent = 1500.00
                });
                model.Houses.Add(new HouseViewModel()
                {
                    HouseId = new Guid(),
                    Address = "4 Golfclub Rd",
                    Rent = 320
                });
                model.Houses.Add(new HouseViewModel()
                {
                    HouseId = new Guid(),
                    Address = "98 Bayview Dr",
                    Rent = 200
                });

                success = true;
            }
            catch (Exception)
            {
            }
            
            return (Success: success, Model: model);
        }

        
    }
}
