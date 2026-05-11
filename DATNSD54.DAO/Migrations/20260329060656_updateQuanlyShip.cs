using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATNSD54.DAO.Migrations
{
    /// <inheritdoc />
    public partial class updateQuanlyShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount_Amount",
                table: "Bill",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ShipCost",
                table: "Bill",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4569));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4264));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4471));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4427));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4433));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4210));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4324));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(3791));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount_Amount",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "ShipCost",
                table: "Bill");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(4029));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3817));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3819));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3981));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(4052));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(4054));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3957));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3960));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3791));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3902));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3903));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3905));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 28, 19, 51, 46, 775, DateTimeKind.Local).AddTicks(3570));
        }
    }
}
