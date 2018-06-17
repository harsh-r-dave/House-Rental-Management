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
    public class FeaturedPhotoService : IFeaturedPhotoService
    {
        private readonly ImageOptions _imageOptions;
        private readonly IHostingEnvironment _env;
        private readonly IFeaturedPhotoRepository _featuredPhotoRepository;
        private readonly ILogger _logger;

        public FeaturedPhotoService(IOptions<ImageOptions> imageOptions,
            IHostingEnvironment env,
            IFeaturedPhotoRepository featuredPhotoRepository,
            ILogger<FeaturedPhotoService> logger)
        {
            _imageOptions = imageOptions.Value;
            _env = env;
            _featuredPhotoRepository = featuredPhotoRepository;
            _logger = logger;
        }

        public async Task<(bool Success, string Error)> UploadFeaturePhotoAsync(FeaturedPhotosViewModel model)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                if (model.Image != null && model.Image.Length > 0)
                {
                    // save image to directory
                    var baseDirectory = _env.WebRootPath;
                    var imageDirectory = string.Format(_imageOptions.FeaturedPhotoPath);
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

                        // save image filename to database
                        FeaturedImage fi = new FeaturedImage()
                        {
                            FileName = model.Image.FileName,
                            CreatedUtc = DateTime.Now,
                            ToDisplay = model.ToDisplay
                        };

                        await _featuredPhotoRepository.SaveFeaturedPhotoAsync(fi);
                    }

                    success = true;
                }
                else
                {
                    error = "Invalid request";
                }
            }
            catch (Exception e)
            {
                error = "Unexpected error occurred while uploading image";
            }

            return (success, error);
        }

        public async Task<(bool Success, string Error, ListFeaturedPhotoViewModel Model)> GetListFeaturedPhotosViewModelAsync()
        {
            bool success = false;
            string error = string.Empty;
            var model = new ListFeaturedPhotoViewModel()
            {
                featuredPhotosCollection = new List<FeaturedPhotosViewModel>()
            };

            try
            {
                var featuredPhotos = await _featuredPhotoRepository.ListAllFeaturedImagesAsync();
                if (featuredPhotos != null)
                {
                    foreach (var photo in featuredPhotos)
                    {
                        // prepare path
                        var destinationPath = Path.Combine(_imageOptions.FeaturedPhotoPath);
                        var fullPath = string.Format("{0}{1}{2}", "/", destinationPath, photo.FileName);

                        model.featuredPhotosCollection.Add(new FeaturedPhotosViewModel()
                        {
                            PhotoId = photo.FeaturedImageId,
                            FileName = photo.FileName,
                            ToDisplay = photo.ToDisplay,
                            FilePath = fullPath
                        });
                    }
                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = "Unexpected error occurred while processing your request";
                _logger.LogError("FeaturedPhotoService/GetListFeaturedPhotosViewModelAsync - exception:{@Ex}", new object[]{
                    ex
                });
            }
            return (Success: success, Error: error, Model: model);
        }

        public async Task<(bool Success, string Error)> DeleteFeaturedImageAsyncById(int id)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                if (id > 0)
                {
                    FeaturedImage image = await _featuredPhotoRepository.FetchFeatureImageByIdAsync(id);
                    if (image != null)
                    {
                        // get directory
                        var baseDirectory = _env.WebRootPath;
                        var destinationPath = Path.Combine(baseDirectory, _imageOptions.FeaturedPhotoPath);
                        var fullPath = string.Format("{0}{1}", destinationPath, image.FileName);
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }

                        // delete record
                        success = await _featuredPhotoRepository.DeleteFeaturedImageAsync(image);
                    }
                    else
                    {
                        error = "Image record not found";
                    }
                }
                else
                {
                    error = "Invalid id";
                }
            }
            catch (Exception ex)
            {
                error = "Unexpected error occurred while processing your request";
                _logger.LogError("FeaturedPhotoService/DeleteFeaturedImageAsyncById - exception:{@Ex}", new object[]{
                    ex
                });
            }

            return (Success: success, Error: error);
        }

        public async Task<(bool Success, string Error, ListFeaturedPhotoViewModel Model)> GetToBeDisplayedFeaturedImagesAsync()
        {
            bool success = false;
            string error = string.Empty;
            var model = new ListFeaturedPhotoViewModel()
            {
                featuredPhotosCollection = new List<FeaturedPhotosViewModel>()
            };

            try
            {
                var featuredPhotos = await _featuredPhotoRepository.ListToBeDisplayedFeaturedImagesAsync();
                if (featuredPhotos != null)
                {
                    foreach (var photo in featuredPhotos)
                    {
                        // prepare path
                        var destinationPath = Path.Combine(_imageOptions.FeaturedPhotoPath);
                        var fullPath = string.Format("{0}{1}{2}", "/", destinationPath, photo.FileName);

                        model.featuredPhotosCollection.Add(new FeaturedPhotosViewModel()
                        {
                            FilePath = fullPath
                        });
                    }
                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = "Unexpected error occurred while processing your request";
                _logger.LogError("FeaturedPhotoService/GetToBeDisplayedFeaturedImagesAsync - exception:{@Ex}", new object[]{
                    ex
                });
            }
            return (Success: success, Error: error, Model: model);
        }

        public async Task<(bool Success, string Error)> ChangeToDisplayStatusByPhotoIdAsync(int imageId, bool toDisplayStatus)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                if (imageId > 0)
                {
                    FeaturedImage image = await _featuredPhotoRepository.FetchFeatureImageByIdAsync(imageId);
                    if (image != null)
                    {
                        image.ToDisplay = toDisplayStatus;
                        success = await _featuredPhotoRepository.SaveFeaturedPhotoAsync(image);
                    }
                    else
                    {
                        error = "Image record not found";
                    }
                }
                else
                {
                    error = "Invalid id";
                }
            }
            catch (Exception ex)
            {
                error = "Unexpected error occurred while processing your request";
                _logger.LogError("FeaturedPhotoService/DeleteFeaturedImageAsyncById - exception:{@Ex}", new object[]{
                    ex
                });
            }

            return (Success: success, Error: error);
        }
    }
}
