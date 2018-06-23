using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class addingfieldstohouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Occupancy",
                table: "House",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlSlug",
                table: "House",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Washrooms",
                table: "House",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupancy",
                table: "House");

            migrationBuilder.DropColumn(
                name: "UrlSlug",
                table: "House");

            migrationBuilder.DropColumn(
                name: "Washrooms",
                table: "House");
        }
    }
}
