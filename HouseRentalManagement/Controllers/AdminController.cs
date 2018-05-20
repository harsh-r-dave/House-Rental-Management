using HouseRentalManagement.Models.AdminViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        public AdminController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        public async Task<IActionResult> Houses()
        {
            SetSiteMessage(messageType: MessageType.Error, displayFor: DisplayFor.FullRequest, message: "House List");
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
            SetSiteMessage(messageType: MessageType.Success, displayFor: DisplayFor.FullRequest, message: "Add house");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddHouse(AddHouseViewModel model)
        {
            return View();
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
    }
}
