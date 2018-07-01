using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class seedamenities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Amenity]");

            migrationBuilder.InsertData("Amenity",
                columns: new[] { "AmenityId", "Description", "ImageFileName" },
                values: new object[] { 1, "Wi-Fi", "wifi.png" });

            migrationBuilder.InsertData("Amenity",
                columns: new[] { "AmenityId", "Description", "ImageFileName" },
                values: new object[] { 2, "Electricity", "electricity.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] {3,  "Heat", "heat.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 4, "Air Conditioner", "ac.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 5, "Water", "water.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 6, "Shower/Bathroom", "washroom.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] {7,  "Fully Furnished", "furnished.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] {8,  "Private Garage", "garage.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 9, "Garbage pick-up", "garbage.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] {10,  "Close to Bus Stop", "bus.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 11, "Closet in Room", "closet.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 12, "Dishwasher", "dishwasher.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 13, "Refrigerator", "fridge.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] {14, "Microwave Oven", "microwave.png" });

            migrationBuilder.InsertData("Amenity",
               columns: new[] { "AmenityId", "Description", "ImageFileName" },
               values: new object[] { 15, "Stove", "stove.png" });

            migrationBuilder.InsertData("Amenity",
              columns: new[] { "AmenityId", "Description", "ImageFileName" },
              values: new object[] {16,  "Television", "tv.png" });

            migrationBuilder.InsertData("Amenity",
              columns: new[] { "AmenityId", "Description", "ImageFileName" },
              values: new object[] {17,  "On-Site Laundry", "washer.png" });

            migrationBuilder.InsertData("Amenity",
              columns: new[] { "AmenityId", "Description", "ImageFileName" },
              values: new object[] { 18, "Wooden Floor", "wooden.png" });

            migrationBuilder.InsertData("Amenity",
            columns: new[] { "AmenityId", "Description", "ImageFileName" },
            values: new object[] { 19, "CCTV", "cctv.png" });

            migrationBuilder.InsertData("Amenity",
            columns: new[] { "AmenityId", "Description", "ImageFileName" },
            values: new object[] {20, "Bed", "bed.png" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
