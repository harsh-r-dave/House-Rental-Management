using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseRentalManagement.Classes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HouseRentalManagement.Controllers
{
    public class HrmController : Controller
    {
        public enum MessageType { Success, Error, Information, Warning };
        public enum DisplayFor { FullRequest, AjaxRequest};

        protected void SetSiteMessage(MessageType messageType, DisplayFor displayFor, string message)
        {
            string key = "";
            switch (messageType)
            {
                case MessageType.Success:
                    key = GetSuccessKey(displayFor);
                    break;
                case MessageType.Error:
                    key = GetErrorKey(displayFor);
                    break;
                case MessageType.Information:
                    key = GetInformationKey(displayFor);
                    break;
                case MessageType.Warning:
                    key = GetWarningKey(displayFor);
                    break;
                default:
                    break;
            }

            TempData[key] = message;
        }

        private string GetErrorKey(DisplayFor displayFor)
        {
            string key = "";
            switch (displayFor)
            {
                case DisplayFor.AjaxRequest:
                    key = Constants.Toastr.Ajax.Error;
                    break;
                case DisplayFor.FullRequest:
                    key = Constants.Toastr.Error;
                    break;
            }

            return key;
        }

        private string GetSuccessKey(DisplayFor displayFor)
        {
            string key = "";
            switch (displayFor)
            {
                case DisplayFor.AjaxRequest:
                    key = Constants.Toastr.Ajax.Success;
                    break;
                case DisplayFor.FullRequest:
                    key = Constants.Toastr.Success;
                    break;
            }
            return key;
        }

        private string GetWarningKey(DisplayFor displayFor)
        {
            string key = "";
            switch (displayFor)
            {
                case DisplayFor.AjaxRequest:
                    key = Constants.Toastr.Ajax.Warning;
                    break;
                case DisplayFor.FullRequest:
                    key = Constants.Toastr.Warning;
                    break;
            }
            return key;
        }

        private string GetInformationKey(DisplayFor displayFor)
        {
            string key = "";
            switch (displayFor)
            {
                case DisplayFor.AjaxRequest:
                    key = Constants.Toastr.Ajax.Information;
                    break;
                case DisplayFor.FullRequest:
                    key = Constants.Toastr.Information;
                    break;
            }
            return key;
        }
    }
}
