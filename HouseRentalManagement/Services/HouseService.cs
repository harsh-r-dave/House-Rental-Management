using HouseRentalManagement.Config;
using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AdminViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.Extensions.Options;
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
        private readonly IAmenityRepository _amenityRepository;
        private readonly ImageOptions _imageOptions;

        public HouseService(IHouseRepository houseRepository,
            IFacilityRepository facilityRepository,
            IAmenityRepository amenityRepository,
            IOptions<ImageOptions> imageOptions)
        {
            _houseRepository = houseRepository;
            _facilityRepository = facilityRepository;
            _amenityRepository = amenityRepository;
            _imageOptions = imageOptions.Value;
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

                success = true;
            }
            catch (Exception)
            {
            }

            return (Success: success, Model: model);
        }

        public async Task<(bool Success, IErrorDictionary Errors, Guid Id)> AddHouseAsync(AddHouseViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();
            var id = Guid.NewGuid();

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
                        CreateUtc = DateTime.UtcNow,
                        AuditUtc = DateTime.UtcNow
                    };

                    // save record
                    var result = await _houseRepository.AddHouseAsync(house);
                    if (result.Success)
                    {
                        id = result.id;
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

            return (Success: success, Errors: errors, Id: id);
        }

        public async Task<(bool Success, IErrorDictionary Errors, EditHouseViewModel Model)> GetEditHouseViewModelAsync(Guid id)
        {
            var success = false;
            var errors = new ErrorDictionary();
            var model = new EditHouseViewModel();

            try
            {
                // fetch house by id
                if (id != Guid.NewGuid())
                {
                    House house = await _houseRepository.FetchHouseByIdAsync(id);
                    if (house != null)
                    {
                        // prepare viewemodel
                        model.AddressLine1 = house.AddressLine1;
                        model.AddressLine2 = house.AddressLine2;
                        model.City = house.City;
                        model.PostalCode = house.PostalCode;
                        model.Province = house.Province;
                        model.Country = house.Country;
                        model.DateAvailableFrom = house.AvailableFrom == DateTime.MinValue ? DateTime.Now : house.AvailableFrom;
                        model.DateAvailableTo = house.AvailableTo == DateTime.MinValue ? DateTime.Now.AddMonths(4) : house.AvailableTo;
                        model.Rent = house.Rent;
                        model.Description = house.Description;
                        model.HouseId = house.HouseId;
                        model.ParkingSpace = house.ParakingSpace;
                    }
                    else
                    {
                        errors.AddError("", "Unable to locate the house details");
                    }

                    // 
                    var amenities = await _amenityRepository.ListAmenitiesAsync();
                    if (amenities != null)
                    {
                        model.Amenities = new List<AmenitiesListViewModel>();
                        foreach (var amenity in amenities)
                        {
                            model.Amenities.Add(new AmenitiesListViewModel
                            {
                                AmenityId = amenity.AmenityId,
                                Title = amenity.Description,
                                Checked = false,
                                ImageSrc = string.Format(_imageOptions.AmenityImagePath, amenity.ImageFileName)
                            });
                        }
                    }

                    // set success
                    success = true;
                }
            }
            catch (Exception)
            {
                errors.AddError("", "Unexpected error occurred while processing your request");
            }

            return (Success: success, Errors: errors, Model: model);
        }

        public async Task<(bool Success, IErrorDictionary Errors)> DeleteHouseAsync(Guid id)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                // fetch facility by id
                House house = await _houseRepository.FetchHouseByIdAsync(id);

                // remove it
                if (house != null)
                {
                    success = await _houseRepository.DeleteHouseAsync(house);
                }
                else
                {
                    errors.AddError("", "Unable to locate house");
                }
            }
            catch (Exception e)
            {
                errors.AddError("", "Unexpected error occured while deleting facility");
            }

            return (Success: success, Errors: errors);
        }

        public async Task<(bool Success, IErrorDictionary Errors)> EditHouseAsync(EditHouseViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                if (model != null)
                {
                    House house = await _houseRepository.FetchHouseByIdAsync(model.HouseId);
                    if (house != null)
                    {
                        house.AddressLine1 = model.AddressLine1;
                        house.AddressLine2 = model.AddressLine2;
                        house.City = model.City;
                        house.PostalCode = model.PostalCode;
                        house.Province = model.Province;
                        house.Country = model.Country;
                        house.AvailableFrom = model.DateAvailableFrom ?? DateTime.Now;
                        house.AvailableTo = model.DateAvailableTo ?? DateTime.Now.AddMonths(4);
                        house.Rent = model.Rent ?? 0;
                        house.Description = model.Description;
                        house.ParakingSpace = model.ParkingSpace;
                        house.AuditUtc = DateTime.UtcNow;
                    }

                    // save record

                    if (await _houseRepository.UpdateHouseAsync(house))
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
                errors.AddError("", "Unexpected error occured while saving data");
            }

            return (Success: success, Errors: errors);
        }
    }
}
