using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AdminViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Controllers
{
    [Authorize]
    public class AdminController : HrmController
    {
        private readonly IHouseService _houseService;
        private readonly IFacilityService _facilityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITenantService _tenantService;
        private readonly IFeaturedPhotoService _featuredPhotoService;

        public AdminController(IHouseService houseService,
           UserManager<ApplicationUser> userManager,
           IFacilityService facilityService,
           ITenantService tenantService,
           IFeaturedPhotoService featuredPhotoService)
        {
            _houseService = houseService;
            _userManager = userManager;
            _facilityService = facilityService;
            _tenantService = tenantService;
            _featuredPhotoService = featuredPhotoService;
        }

        #region House
        public async Task<IActionResult> Houses()
        {
            var model = new ListHouseViewModel();
            var result = await _houseService.ListHousesAsync();
            if (result.Success)
            {
                model = result.Model;
            }
            return View(model);
        }

        public async Task<IActionResult> AddHouse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHouse(AddHouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _houseService.AddHouseAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "House added successfully");
                    return RedirectToAction(nameof(EditHouse), routeValues: new { id = result.Id });
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (var error in result.Errors.GetErrors())
                        {
                            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                        }
                    }
                }
            }
            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, "Please check all the info and try again");
            return View(model);
        }

        public async Task<IActionResult> EditHouse(Guid id)
        {
            var model = new EditHouseViewModel();

            var result = await _houseService.GetEditHouseViewModelAsync(id);
            if (result.Success)
            {
                model = result.Model;
                return View(model);
            }
            else
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors.GetErrors())
                    {
                        SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                    }
                }
            }

            return RedirectToAction(nameof(AdminController.Houses));
        }

        [HttpPost]
        public async Task<IActionResult> EditHouse(EditHouseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _houseService.EditHouseAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "House updated successfully");
                    return RedirectToAction(nameof(Houses));
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (var error in result.Errors.GetErrors())
                        {
                            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                        }
                    }
                }
            }
            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, "Please check all the info and try again");
            return View(model);
        }

        public async Task<IActionResult> DeleteHouse(Guid id)
        {
            var result = await _houseService.DeleteHouseAsync(id);
            if (result.Success)
            {
                SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "House deleted successfully");
            }
            else
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors.GetErrors())
                    {
                        SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(Houses));
        }
        #endregion

        #region Facility
        public async Task<IActionResult> ManageFacility()
        {
            var model = new ManageFacilityViewModel();
            var result = await _facilityService.GetManageFacilityViewModelAsync();
            if (result.Success)
            {
                model = result.Model;
            }
            else
            {
                foreach (var item in result.Errors.GetErrors())
                {
                    SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, item.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFacility(ManageFacilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _facilityService.AddFacilityAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Facility added successfully");
                }
                else
                {
                    SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, "Unexpected error occured");
                }
            }

            // redirect back where list will be populated again
            return RedirectToAction(nameof(ManageFacility));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFacility(ManageFacilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _facilityService.AddFacilityAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Facility updated successfully");
                }
                else
                {
                    SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, "Unexpected error occured");
                }
            }

            // redirect back where list will be populated again
            return RedirectToAction(nameof(ManageFacility));
        }

        public async Task<IActionResult> DeleteFacility(Guid id)
        {
            var result = await _facilityService.DeleteFacilityAsync(id);
            if (result.Success)
            {
                SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Facility deleted successfully");
            }
            else
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors.GetErrors())
                    {
                        SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(ManageFacility));
        }
        #endregion

        #region Amenity
        public async Task<IActionResult> GetHouseAmenities(string houseId)
        {
            var model = await _houseService.GetHouseAmenityViewModelAsync(Guid.Parse(houseId));
            return PartialView("~/Views/Admin/_HouseAmenityPartial.cshtml", model);
        }

        public async Task<IActionResult> UpdateHouseAmenity(HouseAmenityViewModel model)
        {
            var result = await _houseService.UpdateHouseAmenitiesAsync(model);
            if (result.Success)
            {
                SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Amenities saved successfully");
            }
            else
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors.GetErrors())
                    {
                        SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                    }
                }
            }

            return RedirectToAction(nameof(EditHouse), routeValues: new { id = model.HouseId });
        }
        #endregion

        #region House images
        public async Task<IActionResult> UploadHouseImage(AddHouseImageViewModel model)
        {
            var result = await _houseService.UploadHouseImageAsync(model);
            if (result.Success)
            {
                return Json(true);
            }
            return Json(false);
        }

        public async Task<IActionResult> GetHouseImages(Guid houseId)
        {
            var result = await _houseService.FetchHouseImageListAsync(houseId);
            if (result.Success)
            {
                return PartialView("~/Views/Admin/_HouseImageListPartial.cshtml", result.Model);
            }

            return Json(new
            {
                success = result.Success,
                error = result.Error,
                noImage = result.NoImage
            });
        }

        public async Task<IActionResult> DeleteHouseImage(Guid imageId)
        {
            var result = await _houseService.DeleteHouseImageAsync(imageId);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> SetMainHouseImage(Guid houseId, Guid imageId)
        {
            var result = await _houseService.SetHomePageImageAsync(houseId, imageId);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }
        #endregion

        #region House Getting Around
        public async Task<IActionResult> GetHouseGettingAround(Guid houseId)
        {
            var result = await _houseService.FetchHouseGettingAroundByHouseId(houseId);
            if (result.Success)
            {
                return PartialView("~/Views/Admin/_HouseGettingAroundListPartial.cshtml", result.Model);
            }

            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> AddHouseGettingAround(AddHouseGettingAroundViewModel model)
        {
            var result = await _houseService.AddHouseGettingAroundAsync(model);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> DeleteHouseGettingAround(int houseGettingAroundId)
        {
            var result = await _houseService.DeleteHouseGettingAroundAsync(houseGettingAroundId);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }
        #endregion

        #region Tenants
        public IActionResult AddTenant()
        {
            ViewData["Title"] = "Add Tenant";
            return View(new AddTenantViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddTenant(AddTenantViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _tenantService.AddTenantAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Tenant added successfully");
                    return RedirectToAction(nameof(Tenants));
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (var error in result.Errors.GetErrors())
                        {
                            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                        }
                    }
                }
            }
            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, "Please check all the info and try again");
            return View(model);
        }

        public async Task<IActionResult> Tenants()
        {
            var model = new ListTenantViewModel();
            var result = await _tenantService.ListTenantsAsync();
            if (result.Success)
            {
                model = result.Model;
            }
            return View(model);
        }

        public async Task<IActionResult> EditTenant(Guid id)
        {
            ViewData["Title"] = "Edit Tenant";
            var model = new AddTenantViewModel();

            var result = await _tenantService.GetEditTenantViewModelAsync(id);
            if (result.Success)
            {
                model = result.Model;
                return View("~/Views/Admin/AddTenant.cshtml", model);
            }
            else
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors.GetErrors())
                    {
                        SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(AdminController.Tenants));
        }
        [HttpPost]
        public async Task<IActionResult> EditTenant(AddTenantViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _tenantService.AddTenantAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Tenant updated successfully");
                    return RedirectToAction(nameof(Tenants));
                }
                else
                {
                    if (result.Errors != null)
                    {
                        foreach (var error in result.Errors.GetErrors())
                        {
                            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                        }
                    }
                }
            }
            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, "Please check all the info and try again");
            return View("~/Views/Admin/AddTenant.cshtml", model);
        }

        public async Task<IActionResult> GetHouseTenants(Guid houseId)
        {
            var result = await _tenantService.FetchHouseTenantListAsync(houseId);
            if (result.Success)
            {
                return PartialView("~/Views/Admin/_HouseTenantListPartial.cshtml", result.Model);
            }

            return Json(new
            {
                success = result.Success,
                error = result.Error,
                noTenants = result.NoTenants
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddTenantToHouse(Guid houseId, Guid tenantId)
        {
            var result = await _tenantService.AddTenantToHouseAsync(houseId, tenantId);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetTenantListForHouseEditPage(Guid houseId)
        {
            var result = await _tenantService.GetTenantListForHouseEditPageAsync(houseId);
            return Json(new { list = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTenantFromHouse(Guid tenantId, Guid houseId)
        {
            var result = await _tenantService.RemoveTenantFromHouseAsync(tenantId);
            return Json(new { success = result.Success, error = result.Error });
        }
        public async Task<IActionResult> DeleteTenant(Guid id)
        {
            var result = await _tenantService.DeleteTenantAsync(id);
            if (result.Success)
            {
                SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Tenant deleted successfully");
            }
            else
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors.GetErrors())
                    {
                        SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(Tenants));
        }

        [HttpPost]
        public async Task<IActionResult> GetTenantWaitList()
        {
            var result = await _tenantService.GetTenantWaitListDropdownAsync();
            return Json(new { list = result });
        }
        #endregion

        #region Featured Photo
        public IActionResult FeaturedPhotos()
        {
            return View(new FeaturedPhotosViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFeaturedPhoto(FeaturedPhotosViewModel model)
        {
            var result = await _featuredPhotoService.UploadFeaturePhotoAsync(model);
            if (result.Success)
            {
                return Json(true);
            }
            return Json(false);
        }

        public async Task<IActionResult> ListFeaturedPhotos()
        {
            var result = await _featuredPhotoService.GetListFeaturedPhotosViewModelAsync();
            if (result.Success)
            {
                return PartialView("~/Views/Admin/_FeaturedPhotoListPartial.cshtml", result.Model);
            }
            else
            {
                return Json(new
                {
                    success = result.Success,
                    error = result.Error
                });
            }
        }

        public async Task<IActionResult> DeleteFeaturedPhoto(int imageId)
        {
            var result = await _featuredPhotoService.DeleteFeaturedImageAsyncById(imageId);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> ListToBeDisplayedFeaturedPhotos()
        {
            var result = await _featuredPhotoService.GetToBeDisplayedFeaturedImagesAsync();
            if (result.Success)
            {
                return PartialView("~/Views/Admin/_FeaturedPhotosPreviewPartial.cshtml", result.Model);
            }
            else
            {
                return Json(new
                {
                    success = result.Success,
                    error = result.Error
                });
            }
        }

        public async Task<IActionResult> RemoveToDisplayForFeaturedPhoto(int imageId)
        {
            var result = await _featuredPhotoService.ChangeToDisplayStatusByPhotoIdAsync(imageId, toDisplayStatus: false);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> SetToDisplayForFeaturedPhoto(int imageId)
        {
            var result = await _featuredPhotoService.ChangeToDisplayStatusByPhotoIdAsync(imageId, toDisplayStatus: true);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }
        #endregion
    }
}
