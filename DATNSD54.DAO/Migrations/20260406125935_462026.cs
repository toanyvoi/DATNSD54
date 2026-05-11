using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATNSD54.DAO.Migrations
{
    /// <inheritdoc />
    public partial class _462026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "phiShip",
                table: "quanly",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8657));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8658));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8891));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8893));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8727));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8630));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8678));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8681));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 19, 59, 33, 630, DateTimeKind.Local).AddTicks(8426));

            migrationBuilder.InsertData(
                table: "quanly",
                columns: new[] { "ID", "phiShip" },
                values: new object[] { 1, 50000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "quanly",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "phiShip",
                table: "quanly",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3125));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2905));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2907));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3018));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3151));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2992));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2995));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2873));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2932));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2934));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2646));
        }
    }
}
