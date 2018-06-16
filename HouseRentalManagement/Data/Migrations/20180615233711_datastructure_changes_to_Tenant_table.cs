using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class datastructure_changes_to_Tenant_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_House_HouseId",
                table: "Tenant");

            migrationBuilder.AlterColumn<Guid>(
                name: "HouseId",
                table: "Tenant",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Tenant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StayStartDate",
                table: "Tenant",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_House_HouseId",
                table: "Tenant",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_House_HouseId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "StayStartDate",
                table: "Tenant");

            migrationBuilder.AlterColumn<Guid>(
                name: "HouseId",
                table: "Tenant",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_House_HouseId",
                table: "Tenant",
                column: "HouseId",
                principalTable: "House",
                principalColumn: "HouseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
