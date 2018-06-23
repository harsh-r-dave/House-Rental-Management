using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class adding_contactforavailablefrom_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableTo",
                table: "House");

            migrationBuilder.AddColumn<bool>(
                name: "ContactForAvailableFrom",
                table: "House",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactForAvailableFrom",
                table: "House");

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableTo",
                table: "House",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
