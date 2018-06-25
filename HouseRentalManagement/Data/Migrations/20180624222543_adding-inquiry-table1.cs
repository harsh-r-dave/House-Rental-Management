using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class addinginquirytable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inquiry",
                columns: table => new
                {
                    InquiryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Read = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiry", x => x.InquiryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inquiry");
        }
    }
}
