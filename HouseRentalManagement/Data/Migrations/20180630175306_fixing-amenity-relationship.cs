using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class fixingamenityrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenity_HouseAmenity_HouseAmenityId",
                table: "Amenity");

            migrationBuilder.DropIndex(
                name: "IX_Amenity_HouseAmenityId",
                table: "Amenity");

            migrationBuilder.DropColumn(
                name: "HouseAmenityId",
                table: "Amenity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseAmenityId",
                table: "Amenity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenity_HouseAmenityId",
                table: "Amenity",
                column: "HouseAmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenity_HouseAmenity_HouseAmenityId",
                table: "Amenity",
                column: "HouseAmenityId",
                principalTable: "HouseAmenity",
                principalColumn: "HouseAmenityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
