using HouseRentalManagement.Config;
using HouseRentalManagement.Data;
using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.HouseViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services
{
    public class FrontendService : IFrontendService
    {
        private readonly IHouseRepository _houseRepository;
        private ImageOptions _imageOptions;
        private readonly IFeaturedPhotoRepository _featuredPhotoRepository;
        private ILogger _logger;

        public FrontendService(IHouseRepository houseRepository,
            IOptions<ImageOptions> imageOptions,
            IFeaturedPhotoRepository featuredPhotoRepository,
            ILogger<FrontendService> logger)
        {
            _houseRepository = houseRepository;
            _imageOptions = imageOptions.Value;
            _featuredPhotoRepository = featuredPhotoRepository;
            _logger = logger;
        }

        public async Task<IndexViewModel> GetIndexViewModelAsync()
        {
            var model = new IndexViewModel()
            {
                Houses = new List<HouseViewModel>(),
                FeaturedImages = new List<FeaturedPhotosViewModel>()
            };

            try
            {
                // add houses
                var houses = await _houseRepository.GetHouseListForIndexPageAsync();
                if (houses != null)
                {
                    foreach (var house in houses)
                    {
                        model.Houses.Add(GetHouseViewModelFromHouse(house));
                    }
                }

                // add feature photos
                var featuredPhotos = await _featuredPhotoRepository.ListToBeDisplayedFeaturedImagesAsync();
                if (featuredPhotos != null)
                {
                    foreach (var photo in featuredPhotos)
                    {
                        model.FeaturedImages.Add(GetFeaturedPhotosViewModelFromFeatureImage(photo));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("FrontendService/GetIndexViewModelAsync - exception:{@Ex}", new object[]{
                    ex
                });
            }

            return model;
        }

        private HouseViewModel GetHouseViewModelFromHouse(House house)
        {
            var model = new HouseViewModel();

            // set image src
            var imageSrc = string.Empty;
            if (house.HouseImages != null)
            {
                var fileName = house.HouseImages?.Where(a => a.IsHomePageImage.Value).Select(a => a.FileName).FirstOrDefault();
                var imageDirectory = string.Format(_imageOptions.HouseImagePath, house.HouseId);
                imageSrc = string.Format("{0}{1}{2}", "/", imageDirectory, fileName);
            }

            model.DateAvailable = house.ContactForAvailableFrom ? "Please contact" : house.AvailableFrom.ToString("MMM-dd-yyyy");
            model.Description = house.Description;
            model.FullAddress = house.AddressLine1 + ", " + house.City;
            model.Rent = house.Rent > 0 ? house.Rent.ToString("C0") + "/month" : string.Empty;
            model.MainImageSrc = imageSrc;
            model.UrlSlug = house.UrlSlug;

            return model;
        }

        private FeaturedPhotosViewModel GetFeaturedPhotosViewModelFromFeatureImage(FeaturedImage image)
        {
            var model = new FeaturedPhotosViewModel();

            // prepare path
            var destinationPath = Path.Combine(_imageOptions.FeaturedPhotoPath);
            var fullPath = string.Format("{0}{1}{2}", "/", destinationPath, image.FileName);
            model.ImageSrc = fullPath;

            return model;
        }
    }
}
