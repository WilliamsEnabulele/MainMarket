using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainMarket.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
