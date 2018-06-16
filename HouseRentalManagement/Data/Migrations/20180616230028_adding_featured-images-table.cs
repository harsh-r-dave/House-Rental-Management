using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HouseRentalManagement.Data.Migrations
{
    public partial class adding_featuredimagestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeaturedImage",
                columns: table => new
                {
                    FeaturedImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DisplayFromDate = table.Column<DateTime>(nullable: false),
                    DisplayToDate = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    ToDisplay = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedImage", x => x.FeaturedImageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedImage");
        }
    }
}
