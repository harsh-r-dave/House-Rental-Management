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

        public AdminController(IHouseService houseService,
           UserManager<ApplicationUser> userManager,
           IFacilityService facilityService)
        {
            _houseService = houseService;
            _userManager = userManager;
            _facilityService = facilityService;
        }

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

        [HttpPost]
        public async Task AddHouseImage()
        {

        }

        public async Task<IActionResult> Tenants()
        {
            return View(new ListTenantViewModel());
        }

        public async Task AddTenant()
        {

        }

        [HttpPost]
        public async Task AddTenant(AddTenantViewModel model)
        {

        }

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
    }
}
