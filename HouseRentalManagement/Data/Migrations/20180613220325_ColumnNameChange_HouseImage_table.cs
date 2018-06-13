using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class ColumnNameChange_HouseImage_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenity_houseAmenity_HouseAmenityId",
                table: "Amenity");

            migrationBuilder.DropForeignKey(
                name: "FK_House_houseAmenity_HouseAmenityId",
                table: "House");

            migrationBuilder.DropForeignKey(
                name: "FK_houseAmenity_Amenity_AmenityId",
                table: "houseAmenity");

            migrationBuilder.DropForeignKey(
                name: "FK_houseAmenity_House_HouseId",
                table: "houseAmenity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_houseAmenity",
                table: "houseAmenity");

            migrationBuilder.RenameTable(
                name: "houseAmenity",
                newName: "HouseAmenity");

            migrationBuilder.RenameColumn(
                name: "InUse",
                table: "HouseImage",
                newName: "IsHomePageImage");

            migrationBuilder.RenameIndex(
                name: "IX_houseAmenity_HouseId",
                table: "HouseAmenity",
                newName: "IX_HouseAmenity_HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_houseAmenity_AmenityId",
                table: "HouseAmenity",
                newName: "IX_HouseAmenity_AmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HouseAmenity",
                table: "HouseAmenity",
                column: "HouseAmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenity_HouseAmenity_HouseAmenityId",
                table: "Amenity",
                column: "HouseAmenityId",
                principalTable: "HouseAmenity",
                principalColumn: "HouseAmenityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_HouseAmenity_HouseAmenityId",
                table: "House",
                column: "HouseAmenityId",
                principalTable: "HouseAmenity",
                principalColumn: "HouseAmenityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseAmenity_Amenity_AmenityId",
                table: "HouseAmenity",
                column: "AmenityId",
                principalTable: "Amenity",
                principalColumn: "AmenityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseAmenity_House_HouseId",
                table: "HouseAmenity",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenity_HouseAmenity_HouseAmenityId",
                table: "Amenity");

            migrationBuilder.DropForeignKey(
                name: "FK_House_HouseAmenity_HouseAmenityId",
                table: "House");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseAmenity_Amenity_AmenityId",
                table: "HouseAmenity");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseAmenity_House_HouseId",
                table: "HouseAmenity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HouseAmenity",
                table: "HouseAmenity");

            migrationBuilder.RenameTable(
                name: "HouseAmenity",
                newName: "houseAmenity");

            migrationBuilder.RenameColumn(
                name: "IsHomePageImage",
                table: "HouseImage",
                newName: "InUse");

            migrationBuilder.RenameIndex(
                name: "IX_HouseAmenity_HouseId",
                table: "houseAmenity",
                newName: "IX_houseAmenity_HouseId");

            migrationBuilder.RenameIndex(
                name: "IX_HouseAmenity_AmenityId",
                table: "houseAmenity",
                newName: "IX_houseAmenity_AmenityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_houseAmenity",
                table: "houseAmenity",
                column: "HouseAmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenity_houseAmenity_HouseAmenityId",
                table: "Amenity",
                column: "HouseAmenityId",
                principalTable: "houseAmenity",
                principalColumn: "HouseAmenityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_houseAmenity_HouseAmenityId",
                table: "House",
                column: "HouseAmenityId",
                principalTable: "houseAmenity",
                principalColumn: "HouseAmenityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_houseAmenity_Amenity_AmenityId",
                table: "houseAmenity",
                column: "AmenityId",
                principalTable: "Amenity",
                principalColumn: "AmenityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_houseAmenity_House_HouseId",
                table: "houseAmenity",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
