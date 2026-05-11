using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATNSD54.DAO.Migrations
{
    /// <inheritdoc />
    public partial class updateTrangthaiPro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Trang_Thai",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5484));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5227));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5229));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5431));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5532));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5534));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1,
                column: "Trang_Thai",
                value: true);

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5403));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5407));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5197));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5333));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5335));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(5337));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 6, 20, 54, 54, 317, DateTimeKind.Local).AddTicks(4956));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trang_Thai",
                table: "Product");

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
        }
    }
}
