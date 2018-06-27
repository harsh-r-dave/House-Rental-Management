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

        public async Task<IActionResult> GetMessage(int id)
        {
            var result = await _inquiryService.GetMessageByIdAsync(id);
            return Json(new
            {
                success = result.Success,
                error = result.Error,
                message = result.Message,
                isRead = result.IsRead
            });
        }

        public async Task<IActionResult> MarkRead(int id)
        {
            var result = await _inquiryService.MarkMessageReadByIdAsync(id);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> DeleteInquiry(int id)
        {
            var result = await _inquiryService.DeleteInquiryByIdAsync(id);
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> DeleteAllUnread()
        {
            var result = await _inquiryService.DeleteAllUnreadInquiryAsync();
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }

        public async Task<IActionResult> DeleteAllRead()
        {
            var result = await _inquiryService.DeleteAllReadInquiryAsync();
            return Json(new
            {
                success = result.Success,
                error = result.Error
            });
        }
    }
}