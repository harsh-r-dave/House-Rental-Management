using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class Adding_GettingAround_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseGettingAroundId",
                table: "House",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HouseGettingAround",
                columns: table => new
                {
                    HouseGettingAroundId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BikeTime = table.Column<string>(nullable: true),
                    CarTime = table.Column<string>(nullable: true),
                    Create = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<decimal>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false),
                    LocationName = table.Column<string>(nullable: true),
                    WalkingTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseGettingAround", x => x.HouseGettingAroundId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_House_HouseGettingAround_HouseGettingAroundId",
                table: "House");

            migrationBuilder.DropTable(
                name: "HouseGettingAround");

            migrationBuilder.DropIndex(
                name: "IX_House_HouseGettingAroundId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "HouseGettingAroundId",
                table: "House");
        }
    }
}
