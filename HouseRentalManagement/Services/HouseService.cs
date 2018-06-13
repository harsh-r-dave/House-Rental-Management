using HouseRentalManagement.Config;
using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AdminViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IHostingEnvironment _env;
        private readonly IHouseImageRepository _houseImageRepository;
        private readonly ILogger _logger;

        public HouseService(IHouseRepository houseRepository,
            IFacilityRepository facilityRepository,
            IAmenityRepository amenityRepository,
            IOptions<ImageOptions> imageOptions,
            IHostingEnvironment env,
            IHouseImageRepository houseImageRepository,
            ILogger<HouseService> logger)
        {
            _houseRepository = houseRepository;
            _facilityRepository = facilityRepository;
            _amenityRepository = amenityRepository;
            _imageOptions = imageOptions.Value;
            _env = env;
            _houseImageRepository = houseImageRepository;
            _logger = logger;
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

                    // add image viewmodel
                    model.AddHouseImageViewModel = new AddHouseImageViewModel()
                    {
                        HouseId = id
                    };

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

        public async Task<HouseAmenityViewModel> GetHouseAmenityViewModelAsync(Guid houseId)
        {
            var model = new HouseAmenityViewModel();
            try
            {
                model.HouseId = houseId;
                var amenities = await _amenityRepository.ListAmenitiesAsync();
                var houseAmenities = await _amenityRepository.ListHouseAmenitiesByHouseIdAsync(id: houseId);
                if (amenities != null)
                {
                    model.Amenities = new List<AmenitiesListViewModel>();
                    foreach (var amenity in amenities)
                    {
                        model.Amenities.Add(new AmenitiesListViewModel
                        {
                            AmenityId = amenity.AmenityId,
                            Title = amenity.Description,
                            Checked = houseAmenities != null ? houseAmenities.Where(ha => ha.AmenityId == amenity.AmenityId).Any() : false,
                            ImageSrc = string.Format(_imageOptions.AmenityImagePath, amenity.ImageFileName)
                        });
                    }
                }
            }
            catch (Exception)
            {

            }
            return model;
        }

        public async Task<(bool Success, IErrorDictionary Errors)> UpdateHouseAmenitiesAsync(HouseAmenityViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                if (model.Amenities != null)
                {
                    // clear exisiting amenities
                    await _amenityRepository.ClearHouseAmenitiesByHouseIdAsync(model.HouseId);

                    foreach (var item in model.Amenities)
                    {
                        if (item.Checked)
                        {
                            // prepare record
                            HouseAmenity hm = new HouseAmenity()
                            {
                                AmenityId = item.AmenityId,
                                HouseId = model.HouseId
                            };

                            // save record
                            if (!await _amenityRepository.SaveHouseAmenityAsync(hm))
                            {
                                errors.AddError("", "Unable to update amenity");
                            }
                        }
                    }
                }
                else
                {
                    errors.AddError("", "Unable to process your request");
                }
                success = true;
            }
            catch (Exception e)
            {
                errors.AddError("", "Unexpected error occurred while updating amenities");
            }

            return (success, errors);
        }

        public async Task<(bool Success, IErrorDictionary Errors)> UploadHouseImageAsync(AddHouseImageViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                if (model.Image != null && model.Image.Length > 0)
                {
                    // save image to directory
                    var baseDirectory = _env.WebRootPath;
                    var imageDirectory = string.Format(_imageOptions.HouseImagePath, model.HouseId);
                    var destinationPath = Path.Combine(baseDirectory, imageDirectory);
                    // make sure the folder exists
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }

                    var fullPath = string.Format("{0}{1}", destinationPath, model.Image.FileName);

                    if (!File.Exists(fullPath))
                    {
                        using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                        {
                            await model.Image.CopyToAsync(fs);
                        }

                        // determine whether it should be main image or not
                        if (model.IsHomePageImage)
                        {
                            // reset house image
                            await ResetMainImageByHouseIdAsync(model.HouseId);
                        }
                        else
                        {
                            // check if house has any main image
                            var mainImage = await _houseImageRepository.GetMainImageByHouseIdAsync(model.HouseId);
                            model.IsHomePageImage = mainImage == null;
                        }

                        // save image filename to database
                        HouseImage hi = new HouseImage()
                        {
                            HouseId = model.HouseId,
                            FileName = model.Image.FileName,
                            CreateUtc = DateTime.Now,
                            IsHomePageImage = model.IsHomePageImage
                        };

                        await _houseImageRepository.SaveHouseImageAsync(hi);
                    }                    

                    success = true;
                }
            }
            catch (Exception e)
            {
                errors.AddError("", "Unexpected error occurred while uploading image");
            }

            return (success, errors);
        }

        public async Task<(bool Success, String Error, bool NoImage, ListHouseImageViewModel Model)> FetchHouseImageListAsync(Guid houseId)
        {
            bool success = false;
            var error = string.Empty;
            var model = new ListHouseImageViewModel()
            {
                HouseImages = new List<HouseImageViewModel>()
            };
            bool noImage = false;

            try
            {
                if (houseId != Guid.NewGuid())
                {
                    var houseImages = await _houseImageRepository.ListHouseImagesAsync(houseId);
                    if (houseImages != null && houseImages.Any())
                    {
                        foreach (var image in houseImages)
                        {
                            // save image to directory
                            var imageDirectory = string.Format(_imageOptions.HouseImagePath, image.HouseId);
                            var fullPath = string.Format("{0}{1}{2}", "/", imageDirectory, image.FileName);

                            model.HouseImages.Add(new HouseImageViewModel()
                            {
                                ImageId = image.HouseImageId,
                                HouseId = image.HouseId,
                                ImageSrc = fullPath,
                                fileName = image.FileName,
                                isHomePageImage = image.IsHomePageImage ?? false
                            });
                        }

                        success = true;
                    }
                    else
                    {
                        noImage = true;
                        error = "This house doesn't have any photos, please consider uploading some photos for this house under Photos tab.";
                    }
                }
                else
                {
                    error = "Invalid house Id";
                }
            }
            catch (Exception)
            {
                error = "Unexpected error occurred while processing your request";
            }

            return (Success: success, Error: error, NoImage: noImage, Model: model);
        }

        public async Task<(bool Success, string Error)> DeleteHouseImageAsync(Guid imageId)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                // update record
                var houseImage = await _houseImageRepository.FetchHouseImageByHouseImageId(imageId);
                if (houseImage != null)
                {
                    success = await _houseImageRepository.DeleteHouseImageAsync(houseImage);
                }
                else
                {
                    error = "Image not found";
                }

                // delete actual file
                // save image to directory
                var baseDirectory = _env.WebRootPath;
                var imageDirectory = string.Format(_imageOptions.HouseImagePath, houseImage.HouseId);
                var destinationPath = Path.Combine(baseDirectory, imageDirectory);
                var fullPath = string.Format("{0}{1}", destinationPath, houseImage.FileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                error = "Unexpected error occurred while processing your request";
                _logger.LogError("HouseService/DeleteHouseImageAsync - exception:{@Ex}", new object[]{
                    ex
                });
            }
            return (success, error);
        }

        public async Task<(bool Success, string Error)> SetHomePageImageAsync(Guid houseId, Guid imageId)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                // reset house image
                await ResetMainImageByHouseIdAsync(houseId);

                // update record
                var houseImage = await _houseImageRepository.FetchHouseImageByHouseImageId(imageId);
                if (houseImage != null)
                {
                    houseImage.IsHomePageImage = true;
                    await _houseImageRepository.SaveHouseImageAsync(houseImage);
                    success = true;
                }
                else
                {
                    error = "Image not found";
                }
            }
            catch (Exception ex)
            {
                error = "Unexpected error occurred while processing your request";
                _logger.LogError("HouseService/DeleteHouseImageAsync - exception:{@Ex}", new object[]{
                    ex
                });
            }
            return (success, error);
        }

        private async Task ResetMainImageByHouseIdAsync(Guid houseId)
        {
            // reset homepage image for house
            var houseImages = await _houseImageRepository.ListHouseImagesAsync(houseId);
            if (houseImages != null)
            {
                foreach (var image in houseImages)
                {
                    image.IsHomePageImage = false;

                    await _houseImageRepository.SaveHouseImageAsync(image);
                }
            }
        }
    }
}
