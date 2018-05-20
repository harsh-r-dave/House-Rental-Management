using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HouseRentalManagement.Controllers
{
    public class HouseController : HrmController
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TestTemp()
        {
            return View();
        }

        public IActionResult HouseInfo()
        {
            return View();
        }
    }
}
