using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NHISWeb.Migrations
{
    public partial class NewNHIS_Portal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19809185-c5e6-4c89-87d8-d9b786f03a95");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94c6ffc1-a20d-4669-9a2b-0a00c0995816");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97d1c9ab-52c5-429c-afbd-6d1d532a412d", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e265847d-c96e-4369-bc81-c769934bd600", "2", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97d1c9ab-52c5-429c-afbd-6d1d532a412d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e265847d-c96e-4369-bc81-c769934bd600");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19809185-c5e6-4c89-87d8-d9b786f03a95", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94c6ffc1-a20d-4669-9a2b-0a00c0995816", "2", "User", "User" });
        }
    }
}
