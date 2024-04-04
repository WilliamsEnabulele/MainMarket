using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainMarket.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedRoless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0456a5f1-b6d5-4413-8fdc-2ddbcb1c4bf6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0bd116ed-4efe-4069-9451-9278ee24c252");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8e20376-6d08-4401-b86d-23d6ea41b5dc");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "0456a5f1-b6d5-4413-8fdc-2ddbcb1c4bf6", null, "ADMIN", null },
                    { "0bd116ed-4efe-4069-9451-9278ee24c252", null, "BUSINESS", null },
                    { "a8e20376-6d08-4401-b86d-23d6ea41b5dc", null, "CUSTOMER", null }
                });
        }
    }
}
