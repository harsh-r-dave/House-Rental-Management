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

        public IActionResult HouseInfo()
        {
            return View();
        }
    }
}
