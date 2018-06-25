using HouseRentalManagement.Models.InquiryViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Controllers
{
    public class InquiryController : HrmController
    {
        private readonly IInquiryService _inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetReadInquiries()
        {
            var model = await _inquiryService.GetReadInquiriesAsync();
            return PartialView("~/Views/Inquiry/Inquiries.cshtml", model);
        }

        public async Task<IActionResult> GetUnreadInquiries()
        {
            var model = await _inquiryService.GetUnreadInquiriesAsync();
            return PartialView("~/Views/Inquiry/Inquiries.cshtml", model);
        }

        public async Task<IActionResult> SubmitInquiry(AddInquiryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _inquiryService.SubmitNewInquiryAsync(model);
                if (result.Success)
                {
                    return Json(new
                    {
                        success = result.Success
                    });
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

            return Json(new
            {
                success = false,
                error = "Please review the information and submit again"
            });
        }
    }
}