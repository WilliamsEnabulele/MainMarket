using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MainMarket.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedCouponTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "CreatedAt", "DiscountAmount", "MinAmount", "UpdatedAt" },
                values: new object[,]
                {
                    { "40dc8db0-781d-47ba-adea-49c1670412c7", "20OFF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20m, 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "7586c948-900a-40f3-9713-2047033fa033", "10OFF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: "40dc8db0-781d-47ba-adea-49c1670412c7");

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: "7586c948-900a-40f3-9713-2047033fa033");
        }
    }
}
