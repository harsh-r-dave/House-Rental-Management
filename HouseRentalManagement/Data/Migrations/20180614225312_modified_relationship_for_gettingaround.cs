using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class modified_relationship_for_gettingaround : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_House_HouseGettingAround_HouseGettingAroundId",
                table: "House");

            migrationBuilder.DropIndex(
                name: "IX_House_HouseGettingAroundId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "HouseGettingAroundId",
                table: "House");

            migrationBuilder.CreateIndex(
                name: "IX_HouseGettingAround_HouseId",
                table: "HouseGettingAround",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseGettingAround_House_HouseId",
                table: "HouseGettingAround",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseGettingAround_House_HouseId",
                table: "HouseGettingAround");

            migrationBuilder.DropIndex(
                name: "IX_HouseGettingAround_HouseId",
                table: "HouseGettingAround");

            migrationBuilder.AddColumn<int>(
                name: "HouseGettingAroundId",
                table: "House",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_House_HouseGettingAroundId",
                table: "House",
                column: "HouseGettingAroundId");

            migrationBuilder.AddForeignKey(
                name: "FK_House_HouseGettingAround_HouseGettingAroundId",
                table: "House",
                column: "HouseGettingAroundId",
                principalTable: "HouseGettingAround",
                principalColumn: "HouseGettingAroundId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
