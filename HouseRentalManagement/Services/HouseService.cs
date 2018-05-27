using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
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
        private readonly IHouseRepository _houseRepository;
        private readonly IFacilityRepository _facilityRepository;

        public HouseService(IHouseRepository houseRepository,
            IFacilityRepository facilityRepository)
        {
            _houseRepository = houseRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<(bool Success, ListHouseViewModel Model)> ListHousesAsync()
        {
            bool success = false;
            var model = new ListHouseViewModel();

            try
            {
                // get all the house
                var houses = await _houseRepository.ListHousesAsync();
                if (houses != null)
                {
                    foreach (var house in houses)
                    {
                        var parts = new string[] { house.AddressLine1, house.AddressLine2 };
                        var address = string.Join(",", parts.Where(a => !string.IsNullOrEmpty(a)));
                        model.Houses.Add(new HouseViewModel()
                        {
                            HouseId = house.HouseId,
                            Address = address,
                            Rent = house.Rent
                        });
                    }
                }

                //model.Houses.Add(new HouseViewModel()
                //{
                //    HouseId = new Guid(),
                //    Address = "123 Main Street",
                //    Rent = 350.00
                //});
                //model.Houses.Add(new HouseViewModel()
                //{
                //    HouseId = new Guid(),
                //    Address = "123 Main Street",
                //    Rent = 300.00
                //});
                //model.Houses.Add(new HouseViewModel()
                //{
                //    HouseId = new Guid(),
                //    Address = "456 Toronto Street",
                //    Rent = 350.00
                //});
                //model.Houses.Add(new HouseViewModel()
                //{
                //    HouseId = new Guid(),
                //    Address = "11 College Cr",
                //    Rent = 450.00
                //});
                //model.Houses.Add(new HouseViewModel()
                //{
                //    HouseId = new Guid(),
                //    Address = "Some big house",
                //    Rent = 1500.00
                //});
                //model.Houses.Add(new HouseViewModel()
                //{
                //    HouseId = new Guid(),
                //    Address = "4 Golfclub Rd",
                //    Rent = 320
                //});
                //model.Houses.Add(new HouseViewModel()
                //{
                //    HouseId = new Guid(),
                //    Address = "98 Bayview Dr",
                //    Rent = 200
                //});

                success = true;
            }
            catch (Exception)
            {
            }

            return (Success: success, Model: model);
        }

        public async Task<(bool Success, IErrorDictionary Errors)> AddHouseAsync(AddHouseViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                if (model != null)
                {
                    // prepare house
                    House house = new House()
                    {
                        AddressLine1 = model.AddressLine1,
                        AddressLine2 = model.AddressLine2,
                        City = model.City,
                        PostalCode = model.PostalCode,
                        Province = model.Province,
                        Country = model.Country,
                        AvailableFrom = model.DateAvailableFrom ?? DateTime.Now,
                        AvailableTo = model.DateAvailableTo ?? DateTime.Now.AddMonths(4),
                        Rent = model.Rent ?? 0,
                        Description = model.Description,
                        CreateUtc = DateTime.UtcNow,
                        AuditUtc = DateTime.UtcNow
                    };

                    // save record
                    if (await _houseRepository.AddHouseAsync(house))
                    {
                        success = true;
                    }
                    else
                    {
                        errors.AddError("", "Unexpected error occured while saving data");
                    }
                }
                else
                {
                    errors.AddError("", "Invalid data submitted");
                }
            }
            catch (Exception e)
            {

            }

            return (Success: success, Errors: errors);
        }        
    }
}
