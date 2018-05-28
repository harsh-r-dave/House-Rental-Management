using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Classes
{
    public class Constants
    {
        public static class Toastr
        {
            public const string Success = "Toastr.Success";
            public const string Error = "Toastr.Error";
            public const string Warning = "Toastr.Warning";
            public const string Information = "Toastr.Information";

            public class Ajax
            {
                public const string Success = "Toastr.Ajax.Success";
                public const string Warning = "Toastr.Ajax.Warning";
                public const string Error = "Toastr.Ajax.Error";
                public const string Information = "Toastr.Ajax.Information";
            }
        }

        public static class ViewComponentKeys
        {
            public const string SiteAjaxMessageView = "SiteAjaxMessageView";
            public const string SiteMessageView = "SiteMessageView";
        }

        public static class Amenity
        {
            public const string WiFi = "Wi-Fi";
            public const string Electricity = "Electricity";
            public const string Heat = "Heat";
            public const string AirConditioning = "Air Conditioning";
            public const string Water = "Water";
            public const string ShowerBathroom = "Shower/Bathroom";
            public const string Laundry = "Laundry";
            public const string FurnishedRooms = "Furnished Rooms and House";
            public const string KitchenFurnished = "Kitchen Furnished";
            public const string Appliances = "Appliances";
        }
    }
}
