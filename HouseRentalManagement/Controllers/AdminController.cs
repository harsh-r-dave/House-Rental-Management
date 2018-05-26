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
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IHouseService houseService,
           UserManager<ApplicationUser> userManager)
        {
            _houseService = houseService;
            _userManager = userManager;
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
                var user = await _userManager.GetUserAsync(User);
                var result = await _houseService.AddHouseAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "House added successfully");
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFacility(ManageFacilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _houseService.AddFacilityAsync(model);
                if (result.Success)
                {
                    SetSiteMessage(MessageType.Success, DisplayFor.FullRequest, "Facility added successfully");
                }
                else
                {
                    SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, "Unexpected error occured");
                }
            }
            return View("~/Views/Admin/ManageFacility.cshtml", model);
        }
    }
}
