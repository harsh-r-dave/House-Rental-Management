using HouseRentalManagement.Models.AdminViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(new ListHouseViewModel());
        }

        public async Task<IActionResult> AddHouse()
        {
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
