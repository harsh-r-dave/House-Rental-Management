using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class InitialDatabaseSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseFacility",
                columns: table => new
                {
                    HouseFacilityId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<Guid>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseFacility", x => x.HouseFacilityId);
                });

            migrationBuilder.CreateTable(
                name: "Facility",
                columns: table => new
                {
                    FacilityId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: true),
                    HouseFacilityId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facility", x => x.FacilityId);
                    table.ForeignKey(
                        name: "FK_Facility_HouseFacility_HouseFacilityId",
                        column: x => x.HouseFacilityId,
                        principalTable: "HouseFacility",
                        principalColumn: "HouseFacilityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    HouseId = table.Column<Guid>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: false),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AuditUtc = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    CreateUtc = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    HouseFacilityId = table.Column<Guid>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Rent = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.HouseId);
                    table.ForeignKey(
                        name: "FK_House_HouseFacility_HouseFacilityId",
                        column: x => x.HouseFacilityId,
                        principalTable: "HouseFacility",
                        principalColumn: "HouseFacilityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HouseImage",
                columns: table => new
                {
                    HouseImageId = table.Column<Guid>(nullable: false),
                    CreateUtc = table.Column<DateTime>(nullable: true),
                    FileName = table.Column<string>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false),
                    InUse = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseImage", x => x.HouseImageId);
                    table.ForeignKey(
                        name: "FK_HouseImage_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Occupation = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ReferenceName = table.Column<string>(nullable: true),
                    ReferencePhone = table.Column<string>(nullable: true),
                    ReferencedEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.TenantId);
                    table.ForeignKey(
                        name: "FK_Tenant_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "HouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facility_HouseFacilityId",
                table: "Facility",
                column: "HouseFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_House_HouseFacilityId",
                table: "House",
                column: "HouseFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseFacility_FacilityId",
                table: "HouseFacility",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseFacility_HouseId",
                table: "HouseFacility",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseImage_HouseId",
                table: "HouseImage",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_HouseId",
                table: "Tenant",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseFacility_Facility_FacilityId",
                table: "HouseFacility",
                column: "FacilityId",
                principalTable: "Facility",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseFacility_House_HouseId",
                table: "HouseFacility",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facility_HouseFacility_HouseFacilityId",
                table: "Facility");

            migrationBuilder.DropForeignKey(
                name: "FK_House_HouseFacility_HouseFacilityId",
                table: "House");

            migrationBuilder.DropTable(
                name: "HouseImage");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "HouseFacility");

            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "House");
        }
    }
}
