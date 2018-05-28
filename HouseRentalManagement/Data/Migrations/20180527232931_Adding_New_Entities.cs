using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class Adding_New_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmenityId",
                table: "House",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseAmenityId",
                table: "House",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HouseLeaseLengthId",
                table: "House",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParakingSpace",
                table: "House",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LeaseLengths",
                columns: table => new
                {
                    LeaseLengthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseLengths", x => x.LeaseLengthId);
                });

            migrationBuilder.CreateTable(
                name: "HouseLeaseLengths",
                columns: table => new
                {
                    HouseLeaseLengthId = table.Column<int>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false),
                    LeaseLengthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseLeaseLengths", x => x.HouseLeaseLengthId);
                    table.ForeignKey(
                        name: "FK_HouseLeaseLengths_LeaseLengths_HouseLeaseLengthId",
                        column: x => x.HouseLeaseLengthId,
                        principalTable: "LeaseLengths",
                        principalColumn: "LeaseLengthId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "houseAmenity",
                columns: table => new
                {
                    HouseAmenityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmenityId = table.Column<int>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houseAmenity", x => x.HouseAmenityId);
                    table.ForeignKey(
                        name: "FK_houseAmenity_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amenity",
                columns: table => new
                {
                    AmenityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    HouseAmenityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenity", x => x.AmenityId);
                    table.ForeignKey(
                        name: "FK_Amenity_houseAmenity_HouseAmenityId",
                        column: x => x.HouseAmenityId,
                        principalTable: "houseAmenity",
                        principalColumn: "HouseAmenityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_House_AmenityId",
                table: "House",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_House_HouseAmenityId",
                table: "House",
                column: "HouseAmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_House_HouseLeaseLengthId",
                table: "House",
                column: "HouseLeaseLengthId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenity_HouseAmenityId",
                table: "Amenity",
                column: "HouseAmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_houseAmenity_AmenityId",
                table: "houseAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_houseAmenity_HouseId",
                table: "houseAmenity",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_House_Amenity_AmenityId",
                table: "House",
                column: "AmenityId",
                principalTable: "Amenity",
                principalColumn: "AmenityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_houseAmenity_HouseAmenityId",
                table: "House",
                column: "HouseAmenityId",
                principalTable: "houseAmenity",
                principalColumn: "HouseAmenityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_HouseLeaseLengths_HouseLeaseLengthId",
                table: "House",
                column: "HouseLeaseLengthId",
                principalTable: "HouseLeaseLengths",
                principalColumn: "HouseLeaseLengthId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_houseAmenity_Amenity_AmenityId",
                table: "houseAmenity",
                column: "AmenityId",
                principalTable: "Amenity",
                principalColumn: "AmenityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_House_Amenity_AmenityId",
                table: "House");

            migrationBuilder.DropForeignKey(
                name: "FK_House_houseAmenity_HouseAmenityId",
                table: "House");

            migrationBuilder.DropForeignKey(
                name: "FK_House_HouseLeaseLengths_HouseLeaseLengthId",
                table: "House");

            migrationBuilder.DropForeignKey(
                name: "FK_Amenity_houseAmenity_HouseAmenityId",
                table: "Amenity");

            migrationBuilder.DropTable(
                name: "HouseLeaseLengths");

            migrationBuilder.DropTable(
                name: "LeaseLengths");

            migrationBuilder.DropTable(
                name: "houseAmenity");

            migrationBuilder.DropTable(
                name: "Amenity");

            migrationBuilder.DropIndex(
                name: "IX_House_AmenityId",
                table: "House");

            migrationBuilder.DropIndex(
                name: "IX_House_HouseAmenityId",
                table: "House");

            migrationBuilder.DropIndex(
                name: "IX_House_HouseLeaseLengthId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "AmenityId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "HouseAmenityId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "HouseLeaseLengthId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ParakingSpace",
                table: "House");
        }
    }
}
