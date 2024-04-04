using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainMarket.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class movetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60822a8f-cf41-49cf-92f0-a1018a24c85a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88b49107-d2d8-4765-ac3c-d8471b7db52d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eacde577-d1a4-4d5c-8709-d7d7fc614acc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a66f4551-d5ff-4c34-b777-222caba47966", null, "ADMIN", "ADMIN" },
                    { "f042cc1a-bc55-4565-81f6-62a829c4d9f4", null, "CUSTOMER", "CUSTOMER" },
                    { "f13db2eb-145e-40a8-9a4d-8b6150fcc9ea", null, "BUSINESS", "BUSINESS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a66f4551-d5ff-4c34-b777-222caba47966");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f042cc1a-bc55-4565-81f6-62a829c4d9f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f13db2eb-145e-40a8-9a4d-8b6150fcc9ea");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60822a8f-cf41-49cf-92f0-a1018a24c85a", null, "ADMIN", "ADMIN" },
                    { "88b49107-d2d8-4765-ac3c-d8471b7db52d", null, "BUSINESS", "BUSINESS" },
                    { "eacde577-d1a4-4d5c-8709-d7d7fc614acc", null, "CUSTOMER", "CUSTOMER" }
                });
        }
    }
}
