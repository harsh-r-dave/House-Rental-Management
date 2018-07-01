using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HouseRentalManagement.Controllers
{
    public class HouseController : HrmController
    {
        private readonly IFrontendService _frontendService;

        public HouseController(IFrontendService frontendService)
        {
            _frontendService = frontendService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var model = await _frontendService.GetIndexViewModelAsync();
            return View(model);
        }

        public async Task<IActionResult> HouseInfo(string s, Guid houseId)
        {
            var result = await _frontendService.GetHouseInfoViewModelAsync(slug: s, houseId: houseId);
            if (result.Success)
            {
                return View(result.Model);
            }
            
            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, result.Error);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Preview(Guid id)
        {
            var result = await _frontendService.GetHouseInfoViewModelAsync(slug: "", houseId: id);
            if (result.Success)
            {
                return View("~/Views/House/HouseInfo.cshtml", result.Model);
            }

            SetSiteMessage(MessageType.Error, DisplayFor.FullRequest, result.Error);
            return RedirectToAction(nameof(AdminController.EditHouse), controllerName: "Admin", routeValues: new { id = id });
        }
    }
}
