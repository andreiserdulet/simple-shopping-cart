using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Image", "IsDeleted", "ModifiedAt", "Name", "Price" },
                values: new object[] { 1L, "Cars", new DateTimeOffset(new DateTime(2021, 9, 9, 15, 24, 15, 972, DateTimeKind.Unspecified).AddTicks(6742), new TimeSpan(0, 0, 0, 0, 0)), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean feugiat leo aliquet purus tempor congue. Nam ut lacus metus. Morbi suscipit, urna at fringilla lobortis, libero justo pharetra odio, sagittis vehicula arcu orci tincidunt est. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.", "", false, new DateTimeOffset(new DateTime(2021, 9, 9, 15, 24, 15, 972, DateTimeKind.Unspecified).AddTicks(7747), new TimeSpan(0, 0, 0, 0, 0)), "Dacia Logan", 10000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Image", "IsDeleted", "ModifiedAt", "Name", "Price" },
                values: new object[] { 2L, "Cars", new DateTimeOffset(new DateTime(2021, 9, 9, 15, 24, 15, 974, DateTimeKind.Unspecified).AddTicks(8717), new TimeSpan(0, 0, 0, 0, 0)), "Proin dignissim nisi id urna laoreet malesuada tristique ac eros. Aliquam vehicula congue scelerisque. Vestibulum ut orci nec lacus porttitor interdum maximus nec est.", "", false, new DateTimeOffset(new DateTime(2021, 9, 9, 15, 24, 15, 974, DateTimeKind.Unspecified).AddTicks(8787), new TimeSpan(0, 0, 0, 0, 0)), "Open Astra K", 16500m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Image", "IsDeleted", "ModifiedAt", "Name", "Price" },
                values: new object[] { 3L, "Cars", new DateTimeOffset(new DateTime(2021, 9, 9, 15, 24, 15, 974, DateTimeKind.Unspecified).AddTicks(8794), new TimeSpan(0, 0, 0, 0, 0)), "Yes", "", false, new DateTimeOffset(new DateTime(2021, 9, 9, 15, 24, 15, 974, DateTimeKind.Unspecified).AddTicks(8796), new TimeSpan(0, 0, 0, 0, 0)), "VW Golf 7", 19500m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
